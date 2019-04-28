﻿using System;

namespace AventStack.ExtentReports.Reporter.Configuration
{
    public class RichViewReporterConfiguration : BasicFileConfiguration
    {
        public RichViewReporterConfiguration(AbstractReporter reporter) : base(reporter)
        { }

        public Theme Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                UserConfigurationMap.Add("theme", Enum.GetName(typeof(Theme), _theme)?.ToLower());
            }
        }
        
        public string CSS
        {
            get => _css;
            set
            {
                _css = value;
                UserConfigurationMap.Add("css", _css);
            }
        }

        public string JS
        {
            get => _js;
            set
            {
                _js = value;
                UserConfigurationMap.Add("js", _js);
            }
        }

        public Boolean EnableTimeline
        {
            get => _enableTimeline;
            set
            {
                _enableTimeline = value;
                UserConfigurationMap.Add("enableTimeline", value.ToString());
            }
        }

        private Theme _theme;
        private string _css;
        private string _js;
        private bool _enableTimeline;
    }
}
