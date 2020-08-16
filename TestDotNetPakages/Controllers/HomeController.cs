using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace TestDotNetPakages.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            //senders number
            string[] sender = { "10008663", "10008663", "10008663", "10008663", "10008663" };
            
            //receptors numbers
            string[] receptor = { "09112345678", "09112345678", "", "09112345678", "09012345678" };

            //list of messages
            string[] message = { "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار" };

            //localids that exist in local database
            string[] localIDs = { new Random().Next(0, 2454).ToString(), new Random().Next(0, 12544).ToString(),
            new Random().Next(0, 45645).ToString(),new Random().Next(0, 2000000).ToString(),
            new Random().Next(0, 123123456).ToString(),};

            //Your Api Key in kavenegar 
            KavenegarApi kavenegar = new KavenegarApi("Your Api Key");

            SendResult result = null;
            List<SendResult> resultList = null;

            StatusResult statusResult = null;
            List<StatusResult> statusResultList = null;

            StatusLocalMessageIdResult StatusLocalMessageIdResultResult = null;
            List<StatusLocalMessageIdResult> StatusLocalMessageIdResultResultList = null;

            CountInboxResult CountInboxResult = null;


            #region SelectAsync
            result = kavenegar.Select("274037533");
            resultList = kavenegar.Select(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region SelectOutboxAsync
            resultList = kavenegar.SelectOutbox(DateTime.Now.AddDays(-1), DateTime.Now);
            resultList = kavenegar.SelectOutbox(DateTime.Now.AddDays(-2));
            #endregion

            #region SendByPostalCodeAsync 
            resultList = kavenegar.SendByPostalCode(4451865169, sender[0], "slama", 0, 10, 0, 16);
            resultList = kavenegar.SendByPostalCode(4451865169, sender[0], "slama", 0, 10, 0, 16, DateTime.Now);
            #endregion

            #region StatusAsync 
            statusResult = kavenegar.Status("1775698101");
            statusResultList = kavenegar.Status(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region StatusLocalMessageIdAsync 
            StatusLocalMessageIdResultResult = kavenegar.StatusLocalMessageId(localIDs[0]);
            StatusLocalMessageIdResultResultList = kavenegar.StatusLocalMessageId(localIDs.ToList());
            #endregion

            #region CancelAsync 
            statusResult = kavenegar.Cancel("1775698101");
            statusResultList = kavenegar.Cancel(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region CountInboxAsync 
            CountInboxResult = kavenegar.CountInbox(DateTime.Now.AddDays(-1), sender[0]);
            CountInboxResult = kavenegar.CountInbox(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1), sender[0]);
            #endregion

            #region CountOutboxAsync 
            CountInboxResult = kavenegar.CountOutbox(DateTime.Now.AddDays(-1));
            CountInboxResult = kavenegar.CountOutbox(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1));
            #endregion

            #region CountPostalCodeAsync 
            List<CountPostalCodeResult> countPostalCodeResult = kavenegar.CountPostalCode(4451865169);
            #endregion

            #region LatestOutboxAsync 
            resultList = kavenegar.LatestOutbox(1);
            resultList = kavenegar.LatestOutbox(1, sender[0]);
            #endregion

            #region LatestOutboxAsync 
            List<ReceiveResult> receiveResult = kavenegar.Receive(sender[0], 0);
            List<ReceiveResult> ReceiveResult = kavenegar.Receive(sender[0], 1);
            #endregion

            #region sendAsync
            result = kavenegar.Send(sender[0], receptor[0], message[0]);
            result = kavenegar.Send(sender[0], receptor[0], message[0], localIDs[0].ToString());
            #endregion

            #region sendArrayAsync
            resultList = kavenegar.SendArray(sender.ToList(), receptor.ToList(), message.ToList(), localIDs[0]);
            resultList = kavenegar.SendArray(sender[0], receptor.ToList(), message.ToList(), localIDs[0]);
            #endregion

            #region VerifyLookupAsync
            
            //verify is template neme, you can create template from here https://panel.kavenegar.com/Client/Verification/Create
            result = kavenegar.VerifyLookup(receptor[0], "123", "verify");
            
            //rate is template neme, you can create template from here https://panel.kavenegar.com/Client/Verification/Create
            result = kavenegar.VerifyLookup(receptor[0], "123", null, null, null, "token20", "rate", VerifyLookupType.Sms);
            #endregion

            return View();
        }
    }
}
