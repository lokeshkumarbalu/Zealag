//------------------------------------------------------------------------------
// <copyright file="AddDocumentationHeaderCmdlet.cs" company="Zealag">
//    Copyright © Zealag 2018. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Zealag.PSModules.Cmdlets
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Add, "DocumentationHeader")]
    [OutputType(typeof(string[]))]
    public class AddDocumentationHeaderCmdlet : Cmdlet
    {
        [Parameter(Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "File name or a regex filter to specify the files to work on.")]
        public string FileName { get; set; }

        [Parameter(Position = 2,
            Mandatory = false,
            HelpMessage = "Destination (folder) where the out files are to be saved.")]
        [ValidateNotNullOrEmpty]
        public string Destination { get; set; }

        private string _company;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            Configuration config = ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location);
            AppSettingsSection appConfig = (AppSettingsSection)config.GetSection("appSettings");
            _company = appConfig.Settings["Company"].Value;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (string.IsNullOrEmpty(FileName))
            {
                Exception exception = new ArgumentException($"Argument {nameof(FileName)}, cannot be null or empty");
                ErrorRecord errorRecord = new ErrorRecord(exception, "StringNullOrEmpty", ErrorCategory.InvalidArgument, FileName);
                ThrowTerminatingError(errorRecord);
            }

            if (!string.IsNullOrEmpty(Destination))
            {
                if (!Directory.Exists(Destination))
                {
                    Exception exception = new ArgumentException($"Argument {nameof(Destination)}, cannot be null or empty");
                    ErrorRecord errorRecord = new ErrorRecord(exception, "Invalid destination", ErrorCategory.InvalidArgument, Destination);
                    ThrowTerminatingError(errorRecord);
                }
            }

            WriteObject(FileName);
        }
    }
}