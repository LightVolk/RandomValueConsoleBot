using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace RandomValueConsoleBot
{

    

    class Program
    {
        private static string[] lines = System.IO.File.ReadAllLines("RandomValuebotToken.txt");//you should have this file and first line should be Token
        private static readonly TelegramBotClient Bot=new TelegramBotClient(lines[0]);
        private static Random _rnd = new Random(DateTime.Today.Second);
        static void Main(string[] args)
        {                       
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;            
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            _rnd = new Random(DateTime.Now.Millisecond);
            var rand1 = _rnd.Next().ToString();
            var rand2 = _rnd.Next().ToString();
            var rand3 = _rnd.Next().ToString();
            var rand4 = _rnd.Next().ToString();
            InlineQueryResult[] results = {
                new InlineQueryResultArticle
                {
                    Id="1",
                    Title="Random value:",
                    Description=rand1,
                    InputMessageContent=new InputTextMessageContent
                    {
                        MessageText=rand1
                    }
                },
                new InlineQueryResultArticle
                {
                    Id="2",
                    Title="Random value:",
                    Description=rand2,
                    InputMessageContent=new InputTextMessageContent
                    {
                        ParseMode=ParseMode.Default,
                        MessageText=rand2
                    }
                },
                new InlineQueryResultArticle
                {
                    Id="3",
                    Title="Random value:",
                    Description=rand3,
                    InputMessageContent=new InputTextMessageContent
                    {
                        ParseMode=ParseMode.Default,
                        MessageText=rand3
                    }
                },
                new InlineQueryResultArticle
                {

                    Id="4",
                    Title="Random value:",
                    Description=rand4,
                    InputMessageContent=new InputTextMessageContent
                    {
                        ParseMode=ParseMode.Default,
                        MessageText=rand4
                    }
                }

            };

            await Bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, results, isPersonal: true, cacheTime: 0);
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received choosen inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }
        
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;


            if(message.Text.StartsWith(CommandConstants.START_COMMAND))
            {                
                await Bot.SendTextMessageAsync(message.Chat.Id, CommandConstants.START_MESSAGE);
            }
            else if(message.Text.StartsWith(CommandConstants.HELP_COMMAND))
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, CommandConstants.START_MESSAGE);
            }
            else if(message.Text.StartsWith(CommandConstants.VALUE))
            {
                _rnd = new Random(DateTime.Now.Millisecond);
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);                
                await Bot.SendTextMessageAsync(message.Chat.Id, _rnd.Next().ToString());
            }
            else
            {
                _rnd = new Random(DateTime.Now.Millisecond);
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Bot.SendTextMessageAsync(message.Chat.Id, _rnd.Next().ToString());
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await Bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }
    }
}