using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchModule.ViewModels;
using System.Threading;

namespace WatchModuleTests
{
    [TestClass]
    public class MainViewModelTests
    {
        static MainViewModel target;

        [TestInitialize]
        public void OnTestInitialize()
        {
            target = new MainViewModel();
        }

        [TestMethod]
        public void AddNewPenaltyTest()
        {
            //  Arrange
            var penalty = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                IsHomeTeam = true,
                PlayerNumber = 17,
                Key = target.GetKeyForNewPenalty()
            };

            //  Act
            target.AddNewPenalty(penalty);

            //  Assert
            Assert.AreEqual(1, target.AllPenalties.Count);
            Assert.IsInstanceOfType(target.AllPenalties[1], typeof(PenaltyInfo));
            Assert.AreEqual(penalty.Duration, target.AllPenalties[1].Duration);
            Assert.AreEqual(penalty.IsHomeTeam, target.AllPenalties[1].IsHomeTeam);
            Assert.AreEqual(penalty.PlayerNumber, target.AllPenalties[1].PlayerNumber);
        }

        [TestMethod]
        public void AddSeveralPenaltiesTest()
        {
            //  Arrange
            var penalty = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                IsHomeTeam = true,
                PlayerNumber = 17,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty);

            var penalty2 = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 5, 0),
                IsHomeTeam = true,
                PlayerNumber = 19,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty2);

            var penalty3 = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                PlayerNumber = 27,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty3);

            var penalty4 = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                PlayerNumber = 27,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty4);

            //  Assert
            Assert.AreEqual(4, target.AllPenalties.Count);
            Assert.AreEqual(2, target.HomePenalties.Count);
            Assert.AreEqual(2, target.AwayPenalties.Count);

            var actualPenalty = target.AllPenalties[1];
            Assert.AreEqual(penalty.Duration, actualPenalty.Duration);
            Assert.AreEqual(penalty.IsHomeTeam, actualPenalty.IsHomeTeam);
            Assert.AreEqual(penalty.PlayerNumber, actualPenalty.PlayerNumber);

            actualPenalty = target.AllPenalties[2];
            Assert.AreEqual(penalty2.Duration, actualPenalty.Duration);
            Assert.AreEqual(penalty2.IsHomeTeam, actualPenalty.IsHomeTeam);
            Assert.AreEqual(penalty2.PlayerNumber, actualPenalty.PlayerNumber);

            actualPenalty = target.AllPenalties[3];
            Assert.AreEqual(penalty3.Duration, actualPenalty.Duration);
            Assert.AreEqual(penalty3.IsHomeTeam, actualPenalty.IsHomeTeam);
            Assert.AreEqual(penalty3.PlayerNumber, actualPenalty.PlayerNumber);

            actualPenalty = target.AllPenalties[4];
            Assert.AreEqual(penalty4.Duration, actualPenalty.Duration);
            Assert.AreEqual(penalty4.IsHomeTeam, actualPenalty.IsHomeTeam);
            Assert.AreEqual(penalty4.PlayerNumber, actualPenalty.PlayerNumber);
        }

        [TestMethod]
        public void RemovePenaltyTest()
        {
            //  Arrange
            var penalty = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                IsHomeTeam = true,
                PlayerNumber = 17,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty);

            var penalty2 = new PenaltyInfo
            {
                Duration = new TimeSpan(0, 2, 0),
                IsHomeTeam = true,
                PlayerNumber = 17,
                Key = target.GetKeyForNewPenalty()
            };
            target.AddNewPenalty(penalty2);

            //  Act
            target.RemovePenalty(penalty);

            //  Assert
            Assert.AreEqual(1, target.AllPenalties.Count);
            Assert.AreEqual(penalty2.Key, target.AllPenalties[penalty2.Key].Key);
        }

        [TestMethod]
        public void StartStopClockTest()
        {
            target.StartClock(false);
            Thread.Sleep(2000);
            target.StopClock();

            Assert.AreEqual(2, target.Clock.ElapsedTimeSpan.Seconds);

            target.ResetClock();
            Assert.AreEqual(0, target.Clock.ElapsedTimeSpan.Seconds);

            var newTime = new TimeSpan(0, 18, 56);
            target.SetClock(newTime);
            Assert.AreEqual(newTime, target.Clock.ElapsedTimeSpan);

            //target.StartClock();
            //Thread.Sleep(2000);
            //target.StopClock();

            //Assert.AreEqual(newTime.Add(new TimeSpan(0, 0, 2)), target.Clock.ElapsedTimeSpan);
        }
    }
}

