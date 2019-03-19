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

    [Cmdlet(VerbsCommon.Add, "DocumentHeader")]
    [OutputType(typeof(string[]))]
    public class AddDocumentHeaderCmdlet : Cmdlet
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

        private string company;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            Configuration config = ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location);
            AppSettingsSection appConfig = (AppSettingsSection)config.GetSection("appSettings");
            this.company = appConfig.Settings["Company"].Value;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (string.IsNullOrEmpty(this.FileName))
            {
                Exception exception = new ArgumentException($"Argument {nameof(this.FileName)}, cannot be null or empty");
                ErrorRecord errorRecord = new ErrorRecord(exception, "StringNullOrEmpty", ErrorCategory.InvalidArgument, this.FileName);
                this.ThrowTerminatingError(errorRecord);
            }

            if (!string.IsNullOrEmpty(this.Destination))
            {
                if (!Directory.Exists(this.Destination))
                {
                    Exception exception = new ArgumentException($"Argument {nameof(this.Destination)}, cannot be null or empty");
                    ErrorRecord errorRecord = new ErrorRecord(exception, "Invalid destination", ErrorCategory.InvalidArgument, this.Destination);
                    this.ThrowTerminatingError(errorRecord);
                }
            }

            this.WriteObject(this.FileName);
        }
    }
}