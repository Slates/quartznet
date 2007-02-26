/* 
* Copyright 2007 OpenSymphony 
* 
* Licensed under the Apache License, Version 2.0 (the "License"); you may not 
* use this file except in compliance with the License. You may obtain a copy 
* of the License at 
* 
*   http://www.apache.org/licenses/LICENSE-2.0 
*   
* Unless required by applicable law or agreed to in writing, software 
* distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
* WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
* License for the specific language governing permissions and limitations 
* under the License.
* 
*/
using System;
using Common.Logging;
using Quartz;

namespace Quartz.Examples.Example6
{
	
	/// <summary> <p>
	/// A job dumb job that will throw a job execution exception
	/// </p>
	/// 
	/// </summary>
	/// <author>  Bill Kratzer
	/// </author>
	public class BadJob1 : IStatefulJob
	{
		
		// Logging
		private static ILog _log = LogManager.GetLogger(typeof(BadJob1));
		
		
		/// <summary>
		/// Called by the <code>Scheduler</code> when a Trigger</code>
		/// fires that is associated with the <code>Job</code>.
		/// </summary>
		public virtual void Execute(JobExecutionContext context)
		{
			System.String jobName = context.JobDetail.FullName;
			_log.Info("---" + jobName + " executing at " + System.DateTime.Now.ToString("r"));
			
			// a contrived example of an exception that
			// will be generated by this job due to a 
			// divide by zero error
			try
			{
				int zero = 0;
				int calculation = 4815 / zero;
			}
			catch (System.Exception e)
			{
				_log.Info("--- Error in job!");
				JobExecutionException e2 = new JobExecutionException(e);
				// this job will refire immediately
				e2.RefireImmediately();
				throw e2;
			}
			
			_log.Info("---" + jobName + " completed at " + System.DateTime.Now.ToString("r"));
		}

	}
}