/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using System;
using System.Collections.Generic;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using ZKEACMS.ExtendField;
using ZKEACMS.Common.Service;
using ZKEACMS.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Easy;
using Newtonsoft.Json;

namespace ZKEACMS.Widget
{
    public class WidgetBase : EditorEntity
    {
        public static Dictionary<string, Type> KnownWidgetModel { get; } = new Dictionary<string, Type>();
        public static Dictionary<string, Type> KnownWidgetService { get; } = new Dictionary<string, Type>();
        private HashSet<string> availableZones = new HashSet<string>();
        [Key]
        public virtual string ID { get; set; }
        public virtual string WidgetName { get; set; }
        public virtual int? Position { get; set; }
        public virtual string LayoutId { get; set; }
        public virtual string PageId { get; set; }
        public virtual int? RuleID { get; set; }
        public virtual string ZoneId { get; set; }
        public virtual bool IsTemplate { get; set; }
        public virtual string Thumbnail { get; set; }
        public virtual bool IsSystem { get; set; }
        public virtual string PartialView { get; set; }
        public virtual string AssemblyName { get; set; }
        public virtual string ServiceTypeName { get; set; }
        public virtual string ViewModelTypeName { get; set; }
        public virtual string FormView { get; set; }
        public virtual string ExtendData { get; set; }
        public virtual string StyleClass { get; set; }
        private string _customClass;
        [NotMapped, JsonIgnore]
        public string CustomClass
        {
            get
            {
                if (_customClass != null)
                {
                    return _customClass;
                }
                InitStyleClass();
                return _customClass;
            }
        }
        private string _customStyle;
        [NotMapped, JsonIgnore]
        public string CustomStyle
        {
            get
            {
                if (_customStyle != null)
                {
                    return _customStyle;
                }
                InitStyleClass();
                return _customStyle;
            }
        }
        [NotMapped, JsonIgnore]
        public string DataSourceLink { get; set; }
        [NotMapped, JsonIgnore]
        public string DataSourceLinkTitle { get; set; }
        private void InitStyleClass()
        {
            if (StyleClass.IsNullOrWhiteSpace())
            {
                _customClass = _customStyle = string.Empty;
            }
            else
            {
                _customClass = CustomRegex.CssStyle().Replace(StyleClass, evaluator =>
                {
                    _customStyle = evaluator.Groups[1].Value;
                    return string.Empty;
                });
            }
        }

        public Type GetViewModelType()
        {
            string key = $"{AssemblyName},{ViewModelTypeName}";
            if (KnownWidgetModel.ContainsKey(key))
            {
                return KnownWidgetModel[key];
            }
            return null;
        }

        public WidgetBase ToWidgetBase()
        {
            return CopyTo(new WidgetBase());
        }
        public WidgetBasePart ToWidgetBasePart()
        {
            return CopyTo(new WidgetBasePart()) as WidgetBasePart;
        }
        public WidgetBase CopyTo(WidgetBase widget)
        {
            if (widget == null) return widget;
            widget.AssemblyName = AssemblyName;
            widget.CreateBy = CreateBy;
            widget.CreatebyName = CreatebyName;
            widget.CreateDate = CreateDate;
            widget.Description = Description;
            widget.ID = ID;
            widget.LastUpdateBy = LastUpdateBy;
            widget.LastUpdateByName = LastUpdateByName;
            widget.LastUpdateDate = LastUpdateDate;
            widget.LayoutId = LayoutId;
            widget.PageId = PageId;
            widget.RuleID = RuleID;
            widget.PartialView = PartialView;
            widget.Position = Position;
            widget.ServiceTypeName = ServiceTypeName;
            widget.Status = Status;
            widget.Title = Title;
            widget.ViewModelTypeName = ViewModelTypeName;
            widget.WidgetName = WidgetName;
            widget.ZoneId = ZoneId;
            widget.FormView = FormView;
            widget.StyleClass = StyleClass;
            widget.IsTemplate = IsTemplate;
            widget.Thumbnail = Thumbnail;
            widget.IsSystem = IsSystem;

            widget.ExtendData = ExtendData;
            return widget;
        }
        public void SetZone(string zoneCode)
        {
            if (ZoneId.IsNullOrEmpty())
            {
                ZoneId = zoneCode;
            }
            availableZones.Add(zoneCode);
        }
        public bool IsInZone(string zoneCode)
        {
            return ZoneId == zoneCode || availableZones.Contains(zoneCode);
        }
    }
}
