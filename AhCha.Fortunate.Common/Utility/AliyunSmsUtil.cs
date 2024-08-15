using System.Text;
using Aliyun.Acs.Core;
using Newtonsoft.Json.Linq;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// 发送阿里云短信服务工具类
    /// 安装包：aliyun-net-sdk-core
    /// 模板请勿公开出去，计费的（RMB）
    /// </summary>
    public class AliyunSmsUtil
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="PhoneNumbers">手机号码</param>
        /// <returns></returns>
        public static string SendSmsCaptcha(string PhoneNumbers, string code)
        {
            throw new Exception("请提供阿里云短信模板SMS_");
            JObject returnRslt = new JObject();
            returnRslt["code"] = code;
            return UnificationSendSms(PhoneNumbers, SignName: "验证码短信", TemplateCode: "短信模板不提供", TemplateParam: returnRslt.ToString());
        }

        /// <summary>
        /// 短信统一发送调用
        /// </summary>
        /// <param name="PhoneNumbers">接收短信的手机号码</param>
        /// <param name="SignName">短信签名名称</param>
        /// <param name="TemplateCode">短信模板CODE</param>
        /// <param name="TemplateParam">短信模板变量对应的实际值</param>
        /// <returns></returns>
        static string UnificationSendSms(string PhoneNumbers, string SignName, string TemplateCode, string TemplateParam)
        {
            throw new Exception("请提供阿里云短信服务必要参数：regionId，accessKeyId，secret");
            IClientProfile profile = DefaultProfile.GetProfile(regionId: "regionId", accessKeyId: "accessKeyId", secret: "secret");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.AddQueryParameters("PhoneNumbers", PhoneNumbers);
            request.AddQueryParameters("SignName", SignName);
            request.AddQueryParameters("TemplateCode", TemplateCode);
            request.AddQueryParameters("TemplateParam", TemplateParam);
            CommonResponse response = client.GetCommonResponse(request);
            return Encoding.Default.GetString(response.HttpResponse.Content);
            #region Try
            //try
            //{
            //    CommonResponse response = client.GetCommonResponse(request);
            //    return Encoding.Default.GetString(response.HttpResponse.Content);
            //}
            //catch (ServerException e)
            //{
            //    Console.WriteLine(e);
            //}
            //catch (ClientException e)
            //{
            //    Console.WriteLine(e);
            //}
            #endregion
        }
    }
}
