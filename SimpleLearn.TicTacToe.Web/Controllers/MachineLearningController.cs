using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleLearn.TicTacToe.Memory;
using SimpleLearn.TicTacToe.Training;
using SimpleLearn.TicTacToe.Web.Models;

namespace SimpleLearn.TicTacToe.Web.Controllers
{
    public class MachineLearningController : ApiController
    {
        private const string gen = "1";

        [HttpPost]
        [Route("api/ml/next-move")]
        public Move NextMove(BoardState state)
        {
            var frontalLobe = new FrontalLobe(new RedisMemory(gen));

            return frontalLobe.NextMove(new Game
            {
                CurrentBoard = state
            });
        }

        [HttpPost]
        [Route("api/ml/train")]
        public HttpResponseMessage Train(TrainingRequest trainingModel)
        {
            var mem = new RedisMemory(gen);
            var trainer = new GameFinishedTrainer(mem, new FrontalLobe(mem));
            trainer.Feedback(trainingModel.Game, trainingModel.Result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}