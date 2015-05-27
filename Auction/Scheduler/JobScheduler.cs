using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auction.Domain.Abstract;
using Auction.Domain.DBase;
using Auction.Infrastructure;
using Ninject;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;

namespace Auction.Scheduler
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<CheckEndJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(1)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}