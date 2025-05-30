﻿using BizFlow.Application.Model.BaseEntities;
namespace BizFlow.Domain.Model.EntityLogs;

public class RouteLog: AuditableEntity
{
    public string Area { get; set; }
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public string RoleId { get; set; }
    public string LanguageId { get; set; }
    public string IpAddress { get; set; }
    public string IsFirstLogin { get; set; }
    public string LoggedInDateTimeUtc { get; set; }
    public string LoggedOutDateTimeUtc { get; set; }
    public string LoginStatus { get; set; }
    public string PageAccessed { get; set; }
    public string SessionId { get; set; }
    public string UrlReferrer { get; set; }
    public string UserId { get; set; }
}
