using System;
using System.IO;
using System.Management.Automation;
using NLog;

namespace Dzakuma.MicroserviceMockup.Standardization
{
	public class ServiceLocator
	{
		private readonly string _startingDirectory;
		private readonly string _executableNamePattern;
		private string _servicePath;
		private static Logger _internalLogger = LogManager.GetCurrentClassLogger();

		public bool IsServiceLocated
		{
			get
			{
				try
				{
					if (!string.IsNullOrEmpty(_servicePath)) { return true; }
					return LocateService() > 0;
				}
				catch (Exception anomaly)
				{
					_internalLogger.Warn(anomaly, "An error occured when trying to find a service.");
					return false;
				}
			}
		}
		public string ServicePath => _servicePath;

		public ServiceLocator(string startingDirectory, string executableNamePattern)
		{
			_startingDirectory = startingDirectory;
			_executableNamePattern = executableNamePattern;
		}

		public int LocateService()
		{
			int servicesFound = 0;
			if (!Directory.Exists(_startingDirectory)){ return servicesFound; }

			using (var posh = PowerShell.Create())
			{
				posh.AddScript(
					"Get-ChildItem -Path '" + _startingDirectory + "' -File -Filter \"*.exe\" -Recurse | " +
					"Where-Object { $_.FullName -like \"*" + _executableNamePattern + "*\" } | " +
					"ForEach-Object { $_.FullName }\r\n"
				);

				foreach (var response in posh.Invoke())
				{
					if (response == null) { continue; }
					servicesFound++;
					_servicePath = response.BaseObject.ToString();
				}
			}

			return servicesFound;
		}
	}
}
