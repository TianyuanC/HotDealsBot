using HotDealsBot.Service;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotDealsBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// LUIS Service
        /// </summary>
        private readonly LuisService luis;
        /// <summary>
        /// Hot Deal Search Service
        /// </summary>
        private readonly HotDealService hotDeal;

        /// <summary>
        /// Constructor
        /// </summary>
        public MessagesController()
        {
            luis = new LuisService();
            hotDeal = new HotDealService();
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                var refinements = await luis.Listen(message.Text);
                var result = await hotDeal.Search(refinements);
                return message.CreateReplyMessage(result);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        /// <summary>
        /// System Messages
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}