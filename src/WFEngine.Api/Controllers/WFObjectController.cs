using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Threading.Channels;
using WFEngine.Activities.Core.Model;
using WFEngine.Api.Dto.Request.WFObject;
using WFEngine.Api.Dto.Response.WFObject;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Enums;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WFObjectController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IStringLocalizer<WFObjectResource> localizer;
        readonly IStringLocalizer<ProjectResource> projectLocalizer;
        readonly IStringLocalizer<SolutionResource> solutionLocalizer;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_projectLocalizer"></param>
        /// <param name="_solutionLocalizer"></param>
        public WFObjectController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<WFObjectResource> _localizer,
            IStringLocalizer<ProjectResource> _projectLocalizer,
            IStringLocalizer<SolutionResource> _solutionLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            projectLocalizer = _projectLocalizer;
            solutionLocalizer = _solutionLocalizer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("insert/{projectId}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Insert(int ProjectId, [FromBody] InsertWFObjectRequestDTO dto)
        {
            InsertWFObjectResponse response = new InsertWFObjectResponse();
            IDataResult<Project> projectExists = uow.Project.GetProject(ProjectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);
            User currentUser = CurrentUser;
            Solution solution = solutionExists.Data;
            WFObject wfObject = mapper.Map<WFObject>(dto);
            wfObject.SolutionId = solution.Id;
            wfObject.ProjectId = ProjectId;
            wfObject.CreatorId = currentUser.Id;
            switch ((enumWFObjectType)wfObject.WfObjectTypeId)
            {
                case enumWFObjectType.WorkFlow:
                    WFWorkflow wfWorkflow = new WFWorkflow();
                    wfWorkflow.UniqueKey = Guid.Parse(wfObject.UniqueKey);
                    wfWorkflow.Name = wfObject.Name;
                    wfWorkflow.Description = wfObject.Description;
                    wfWorkflow.Blocks = new System.Collections.Generic.List<WFBlock>();
                    wfObject.Value = JsonConvert.SerializeObject(wfWorkflow);
                    break;
            }
            IResult wfObjectCreated = uow.WFObject.Insert(wfObject);
            if (!wfObjectCreated.Success)
                return NotFound(response, localizer[wfObjectCreated.Message]);
            if (!uow.Commit())
                return NotFound(response);
            response.Id = wfObject.Id;
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="WFObjectId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{projectId}/{wfObjectId}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Delete(int ProjectId, int WFObjectId)
        {
            DeleteWFObjectResponse response = new DeleteWFObjectResponse();
            IDataResult<Project> projectExists = uow.Project.GetProject(ProjectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);

            IDataResult<WFObject> wfObjectExists = uow.WFObject.FindWFObjectById(WFObjectId);
            if (!wfObjectExists.Success)
                return NotFound(response, localizer[wfObjectExists.Message]);

            IResult isDeleted = uow.WFObject.Delete(wfObjectExists.Data);
            if (!isDeleted.Success)
                return NotFound(response, localizer[isDeleted.Message]);

            if (!uow.Commit())
                return NotFound(response);

            response.IsDeleted = isDeleted.Success;
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="WFObjectId"></param>
        /// <returns></returns>
        [HttpGet("get/{projectId}/{wfObjectId}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Get(int ProjectId, int WFObjectId)
        {
            GetWFObjectResponse response = new GetWFObjectResponse();
            IDataResult<Project> projectExists = uow.Project.GetProject(ProjectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);

            IDataResult<WFObject> wfObjectExists = uow.WFObject.FindWFObjectById(WFObjectId);
            if (!wfObjectExists.Success)
                return NotFound(response, localizer[wfObjectExists.Message]);

            WFObject wfObject = wfObjectExists.Data;

            response.WFObject.Id = wfObject.Id;
            response.WFObject.WfObjectTypeId = wfObject.WfObjectTypeId;
            response.WFObject.Name = wfObject.Name;
            response.WFObject.Description = wfObject.Description;
            response.WFObject.Value = wfObject.Value;

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="WFObjectId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update/{projectId}/{wfObjectId}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Update(int ProjectId, int WFObjectId, [FromBody] UpdateWFObjectRequestDTO dto)
        {
            UpdateWFObjectResponse response = new UpdateWFObjectResponse();

            IDataResult<Project> projectExists = uow.Project.GetProject(ProjectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);

            IDataResult<WFObject> wfObjectExists = uow.WFObject.FindWFObjectById(WFObjectId);
            if (!wfObjectExists.Success)
                return NotFound(response, localizer[wfObjectExists.Message]);

            WFObject wfObject = wfObjectExists.Data;
            wfObject.Name = dto.Name;
            wfObject.Description = dto.Description;

            IResult isUpdated = uow.WFObject.Update(wfObject);

            if (!isUpdated.Success)
                return NotFound(response, localizer[isUpdated.Message]);

            if (!uow.Commit())
                return NotFound(response);

            response.IsUpdated = isUpdated.Success;
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="WfObjectId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("save/{projectId}/{wfObjectId}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Save(int ProjectId,int WfObjectId,[FromBody]SaveWFObjectRequestDTO dto)
        {            
            SaveWFObjectResponse response = new SaveWFObjectResponse();

            IDataResult<Project> projectExists = uow.Project.GetProject(ProjectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;

            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);

            IDataResult<WFObject> wfObjectExists = uow.WFObject.FindWFObjectById(WfObjectId);
            if (!wfObjectExists.Success)
                return NotFound(response, localizer[wfObjectExists.Message]);

            WFObject wfObject = wfObjectExists.Data;
            wfObject.Value = dto.Content;

            IResult isUpdated = uow.WFObject.Update(wfObject);

            if (!isUpdated.Success)
                return NotFound(response, localizer[isUpdated.Message]);

            if (!uow.Commit())
                return NotFound(response);

            response.IsUpdated = isUpdated.Success;

            return Ok(response);
        }
    }
}
