using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework.Internal;
using TennisKata;

namespace TennisKataTests
{
    public class TennisKataTests
    {

        private static IEnumerable<TestCaseData> ServerScoreProgressions = new[]
        {
            new TestCaseData(PlayerScore.Love, PlayerScore.Love, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Love, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Love, PlayerScore.Forty),
            new TestCaseData(PlayerScore.Love, PlayerScore.Fifteen, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Fifteen, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Fifteen, PlayerScore.Forty),
            new TestCaseData(PlayerScore.Love, PlayerScore.Thirty, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Thirty, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Thirty, PlayerScore.Forty)
        };

        private static IEnumerable<TestCaseData> ReturnerScoreProgressions = new[]
        {
            new TestCaseData(PlayerScore.Love, PlayerScore.Love, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Love, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Love, PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Love, PlayerScore.Fifteen, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Fifteen, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Fifteen, PlayerScore.Thirty),
            new TestCaseData(PlayerScore.Love, PlayerScore.Thirty, PlayerScore.Forty),
            new TestCaseData(PlayerScore.Fifteen, PlayerScore.Thirty, PlayerScore.Forty),
            new TestCaseData(PlayerScore.Thirty, PlayerScore.Thirty, PlayerScore.Forty)
        };

        private static IEnumerable<TestCaseData> PreDeuceScore = new[]
        {
            new TestCaseData(PlayerScore.Love),
            new TestCaseData(PlayerScore.Fifteen),
            new TestCaseData(PlayerScore.Thirty)
        };

        [TestCaseSource(nameof(ServerScoreProgressions))]
        public void PreDeuce_ScoreServer_ReturnsCorrectState(PlayerScore serverScore, PlayerScore returnerScore, PlayerScore expectedServerScore)
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(serverScore, returnerScore));
            var newState = TennisKata.Game.ScoreServer(gameState);
            Assert.That(newState.IsPreDeuce);
            switch (newState)
            {
                case GameState.PreDeuce preDeuce:
                    Assert.That(preDeuce.Item.Item1, Is.EqualTo(expectedServerScore));
                    Assert.That(preDeuce.Item.Item2, Is.EqualTo(returnerScore));
                    break;
                default: Assert.Fail("Expected a pre deuce state");
                    break;
            }
        }

        [TestCaseSource(nameof(ReturnerScoreProgressions))]
        public void PreDeuce_ScoreReturner_ReturnsCorrectState(PlayerScore serverScore, PlayerScore returnerScore, PlayerScore expectedReturnerScore)
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(serverScore, returnerScore));
            var newState = TennisKata.Game.ScoreReturner(gameState);
            Assert.That(newState.IsPreDeuce);
            switch (newState)
            {
                case GameState.PreDeuce preDeuce:
                    Assert.That(preDeuce.Item.Item1, Is.EqualTo(serverScore));
                    Assert.That(preDeuce.Item.Item2, Is.EqualTo(expectedReturnerScore));
                    break;
                default:
                    Assert.Fail("Expected a pre deuce state");
                    break;
            }
        }

        [TestCaseSource(nameof(PreDeuceScore))]
        public void PreDeuce_ServerOnForty_ScoreServer_ReturnsServerWin(PlayerScore returnerScore)
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(PlayerScore.Forty, returnerScore));
            var newState = Game.ScoreServer(gameState);
            Assert.That(newState.IsGameWonServer);
        }

        [TestCaseSource(nameof(PreDeuceScore))]
        public void PreDeuce_ReturnerOnForty__ScoreReturner_ReturnsReturnerWin(PlayerScore serverScore)
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(serverScore, PlayerScore.Forty));
            var newState = Game.ScoreReturner(gameState);
            Assert.That(newState.IsGameWonReturner);
        }

        [Test()]
        public void PreDeuce_ScoreServer_AtThirtyForty_ReturnsDeuce()
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(PlayerScore.Thirty, PlayerScore.Forty));
            var newState = TennisKata.Game.ScoreServer(gameState);
            Assert.That(newState.IsDeuce);
        }

        [Test()]
        public void PreDeuce_ScoreReturner_AtFortyThirty_ReturnsDeuce()
        {
            var gameState = GameState.PreDeuce.NewPreDeuce(PreDeuce.NewPreDeuceScore(PlayerScore.Forty, PlayerScore.Thirty));
            var newState = TennisKata.Game.ScoreReturner(gameState);
            Assert.That(newState.IsDeuce);
        }

        [Test()]
        public void Deuce_ScoreServer_ReturnsAdvantageServer()
        {
            var gameState = GameState.Deuce;
            var newState = TennisKata.Game.ScoreServer(gameState);
            Assert.That(newState.IsAdvantageServer);
        }

        [Test()]
        public void Deuce_ScoreReturner_ReturnsAdvantageReturner()
        {
            var gameState = GameState.Deuce;
            var newState = TennisKata.Game.ScoreReturner(gameState);
            Assert.That(newState.IsAdvantageReturner);
        }

        [Test()]
        public void AdvantageServer_ScoreReturner_ReturnsWinServer()
        {
            var gameState = GameState.AdvantageServer;
            var newState = TennisKata.Game.ScoreServer(gameState);
            Assert.That(newState.IsGameWonServer);
        }

        [Test()]
        public void AdvantageServer_ScoreReturner_ReturnsDeuce()
        {
            var gameState = GameState.AdvantageServer;
            var newState = TennisKata.Game.ScoreReturner(gameState);
            Assert.That(newState.IsDeuce);
        }


        [Test()]
        public void AdvantageReturner_ScoreServer_ReturnsDeuce()
        {
            var gameState = GameState.AdvantageReturner;
            var newState = TennisKata.Game.ScoreServer(gameState);
            Assert.That(newState.IsDeuce);
        }

        [Test()]
        public void AdvantageReturner_ScoreReturner_ReturnsWinReturner()
        {
            var gameState = GameState.AdvantageReturner;
            var newState = TennisKata.Game.ScoreReturner(gameState);
            Assert.That(newState.IsGameWonReturner);
        }

    }
}