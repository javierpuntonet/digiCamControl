﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Capture.Workflow.Core.Classes;
using Capture.Workflow.Core.Classes.Attributes;
using Capture.Workflow.Plugins.Views.View;
using Capture.Workflow.Plugins.Views.ViewModel;
using Capture.Workflow.Core;

namespace Capture.Workflow.Plugins.Views
{
    [Description("")]
    [PluginType(PluginType.View)]
    [DisplayName("Live view")]
    public class LiveView : BaseView
    {

        public override WorkFlowView CreateView()
        {
            WorkFlowView view = new WorkFlowView();
            view.Properties.Items.Add(new CustomProperty()
            {
                Name = "ViewTitle",
                PropertyType = CustomPropertyType.String
            });
            view.Events.Add(new CommandCollection("Load"));
            view.Events.Add(new CommandCollection("UnLoad"));
            return view;
        }

        public override List<string> GetPositions()
        {
            return new List<string> { "Left", "BottomLeft", "BottomRight" };
        }


        public override UserControl GetPreview(WorkFlowView view)
        {
            var model = new LiveviewViewModel();
            foreach (var element in view.Elements)
            {
                switch (element.Properties["Position"].Value)
                {
                    case "Left":
                        model.LeftElements.Add(element.Instance.GetControl(element));
                        break;
                    case "BottomLeft":
                        model.BottomLeftElements.Add(element.Instance.GetControl(element));
                        break;

                    case "BottomRight":
                        model.BottomRightElements.Add(element.Instance.GetControl(element));
                        break;
                }
            }
            model.View = view;
            var res = new LiveViewUI();
            res.DataContext = model;
            WorkflowManager.Execute(view.GetEventCommands("Load"));
            return res;
        }
    }
}
