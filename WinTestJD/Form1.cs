using Smart.API.Adapter.Biz;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTestJD
{
    public partial class Form1 : Form
    {
        private HeartService heartService;
        public Form1()
        {
            InitializeComponent();
            LogHelper.RegisterLog4Config(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Log4net.config");
        }

        //启动心跳,白名单更新
        private void button3_Click(object sender, EventArgs e)
        {

            heartService = new HeartService();
            heartService.Start();
            button3.Enabled = false;
        }


        //更新车位总数
        private void button1_Click(object sender, EventArgs e)
        {
            if (heartService == null)
                return;
            heartService.UpdateParkTotalCount();

        }

        //更新剩余车位数
        private void button2_Click(object sender, EventArgs e)
        {
            if (heartService == null)
                return;
            heartService.UpdateParkRemainCount();

        }

        //同步设备状态
        private void button4_Click(object sender, EventArgs e)
        {
            if (heartService == null)
                return;
            heartService.UpdateEquipmentStatus();


        }

        static string inRecordId = "";
        static string inTime = "";
        static string outRecordId = "";

        private void btn_WhiteList_Click(object sender, EventArgs e)
        {

            InRecognitionRecord inrecognition = new InRecognitionRecord();
            inrecognition.inRecordId = inRecordId = txt_LogNo.Text = "07AC2B0012041212101200000" + new Random().Next(100, 999);
            inrecognition.parkId = "";
            inrecognition.inDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
            inrecognition.inDeviceName = "入口";
            inrecognition.recognitionTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            inrecognition.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            inrecognition.plateNumber = txt_plateNumber.Text;
            inrecognition.plateColor = "BLUE";
            inrecognition.reTrySend = "0";

            richText_Msg.Text = inrecognition.ToJson() + "\r\n" + richText_Msg.Text;
            APIResultBase result = new JDParkBiz().CheckWhiteList(inrecognition, enumJDBusinessType.InRecognition);

            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
        }
        private void btn_InRecognition_Click(object sender, EventArgs e)
        {
            // new JDParkBiz().CheckWhiteList();

            InRecognitionRecord inrecognition = new InRecognitionRecord();
            inrecognition.inRecordId = inRecordId = txt_LogNo.Text = "07AC2B0012041212101200000" + new Random().Next(100, 999);
            inrecognition.parkId = "";
            inrecognition.inDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
            inrecognition.inDeviceName = "入口";
            inrecognition.recognitionTime = new DateTime(2018, 6, 11, 8, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
            inrecognition.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            inrecognition.plateNumber = txt_plateNumber.Text;
            inrecognition.plateColor = "BLUE";
            inrecognition.reTrySend = "0";

            richText_Msg.Text = inrecognition.ToJson() + "\r\n" + richText_Msg.Text;
           APIResultBase result = new JDParkBiz().PostInRecognition(inrecognition, enumJDBusinessType.InRecognition);

           richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
          
        }

        private void btn_InCross_Click(object sender, EventArgs e)
        {
            InCrossRecord record = new InCrossRecord();
            record.inRecordId = txt_LogNo.Text;
            record.parkId = "";
            record.InDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
            record.inDeviceName = "入口";
            record.inTime = inTime = new DateTime(2018, 6, 11, 8, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
            record.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            record.plateNumber = txt_plateNumber.Text;
            record.plateColor = "BLUE";
            record.reTrySend = "0";
            record.parkEventType = txt_EventType.Text;
            record.remark = txt_Remark.Text;
            APIResultBase result = new JDParkBiz().PostCarIn(record, enumJDBusinessType.InCross);
            richText_Msg.Text = record.ToJson() + "\r\n" + richText_Msg.Text;
            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
        }

        private void btn_OutRecognition_Click(object sender, EventArgs e)
        {
            OutRecognitionRecord record = new OutRecognitionRecord();
            record.inRecordId = txt_LogNo.Text;
            record.parkId = "";
            record.inDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
            record.inDeviceName = "入口";
            record.inTime = inTime;
            record.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            record.plateNumber = txt_plateNumber.Text;
            record.plateColor = "BLUE";
            record.reTrySend = "0";

            record.outDeviceId = "5A84433D-A79D-4120-A86D-799F80C2A005".ToLower();
            record.outDeviceName = "出口";
            record.outImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            record.outRecordId = outRecordId = "07AC2B001204120F250800000" + new Random().Next(100, 999);
            record.recognitionTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            APIResultBase result = new JDParkBiz().PostOutRecognition(record, enumJDBusinessType.OutRecognition);
            richText_Msg.Text = record.ToJson() + "\r\n" + richText_Msg.Text;
            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
        }

        private void btn_OutCross_Click(object sender, EventArgs e)
        {
            OutCrossRecord record = new OutCrossRecord();
            record.inRecordId = txt_LogNo.Text;
            record.parkId = "";
            record.inDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
            record.inDeviceName = "入口";
            record.inTime = inTime;
            record.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            record.plateNumber = txt_plateNumber.Text;
            record.plateColor = "BLUE";
            record.reTrySend = "0";
            record.parkEventType = txt_EventType.Text;
            record.remark = txt_Remark.Text;
            record.outDeviceId = "5A84433D-A79D-4120-A86D-799F80C2A005".ToLower();
            record.outDeviceName = "出口";
            record.outImage = "http://localhost:8093/Pic/152233_SB1.jpg";
            record.outRecordId = outRecordId;
            record.outTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            APIResultBase result = new JDParkBiz().PostCarOut(record, enumJDBusinessType.OutCross);
            richText_Msg.Text = record.ToJson() + "\r\n" + richText_Msg.Text;
            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
        }

        private void btn_PayCheck_Click(object sender, EventArgs e)
        {
            RequestPayCheck payCheck = new RequestPayCheck();
            payCheck.parkId = "";
            payCheck.payNo = txt_LogNo.Text;
            APIResultBase<ResponsePayCheck> result = new JDParkBiz().PayCheck(payCheck);
            richText_Msg.Text = payCheck.ToJson() + "\r\n" + richText_Msg.Text;
            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;

            if (result.data != null && result.data.payStatus == 0)
            {
                OutCrossRecord record = new OutCrossRecord();
                record.inRecordId = txt_LogNo.Text;
                record.parkId = "";
                record.inDeviceId = "E6C6A279-1389-4F8E-8B75-55ADE585C5CD".ToLower();
                record.inDeviceName = "入口";
                record.inTime = inTime;
                record.inImage = "http://localhost:8093/Pic/152233_SB1.jpg";
                record.plateNumber = txt_plateNumber.Text;
                record.plateColor = "BLUE";
                record.reTrySend = "0";
                record.parkEventType = txt_EventType.Text;
                record.remark = txt_Remark.Text;
                record.outDeviceId = "5A84433D-A79D-4120-A86D-799F80C2A005".ToLower();
                record.outDeviceName = "出口";
                record.outImage = "http://localhost:8093/Pic/152233_SB1.jpg";
                record.outRecordId = outRecordId;
                record.outTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                APIResultBase result1 = new JDParkBiz().PostCarOut(record, enumJDBusinessType.OutCross);
                richText_Msg.Text = record.ToJson() + "\r\n" + richText_Msg.Text;
                richText_Msg.Text = result1.ToJson() + "\r\n" + richText_Msg.Text;
            }
        }

        private void btn_ThirdCharging_Click(object sender, EventArgs e)
        {
            RequestThirdCharging thirdCharing = new RequestThirdCharging();
            thirdCharing.inRecordId = txt_LogNo.Text;
            thirdCharing.plateNumber = txt_plateNumber.Text;
            APIResultBase result = new JDParkBiz().ThirdCharging(thirdCharing);
            richText_Msg.Text = thirdCharing.ToJson() + "\r\n" + richText_Msg.Text;
            richText_Msg.Text = result.ToJson() + "\r\n" + richText_Msg.Text;
        }




    }
}
