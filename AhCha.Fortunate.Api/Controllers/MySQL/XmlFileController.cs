using System.Xml;
using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.Common.Utility;
using Microsoft.AspNetCore.Authorization;

namespace AhCha.Fortunate.Api.Controllers.MySQL
{
    /// <summary>
    /// Xml相关操作
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class XmlFileController : BaseApiController
    {
        /// <summary>
        /// 创建一个xml文件（节点）
        /// </summary>
        /// <param name="FileName">传入文件名称例如：aaa</param>
        /// <returns></returns>
        [HttpGet]
        public Task<string> CreateXml()
        {
            string FileName = "节点";
            //xml文件存放目录
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml");
            //初始化一个xml实例
            XmlDocument XmlDoc = new XmlDocument();
            //创建xml的根节点
            XmlElement rootElement = XmlDoc.CreateElement("BaseFortunateNode");
            //将根节点加入到xml文件中（AppendChild）
            XmlDoc.AppendChild(rootElement);
            //初始化第一层的第一个子节点
            XmlElement firstLevelElement1 = XmlDoc.CreateElement("AesKey");
            //初始化第二层的第一个子节点
            XmlElement secondLevelElement11 = XmlDoc.CreateElement("PublicKey");
            //填充第二层的第一个子节点的值（InnerText）
            secondLevelElement11.InnerText = "测试测试";
            //初始化第二层的第二个子节点的值（InnerText）
            XmlElement secondLevelElement12 = XmlDoc.CreateElement("PrivateKey");
            secondLevelElement12.InnerText = "测试测试";
            firstLevelElement1.AppendChild(secondLevelElement11);
            firstLevelElement1.AppendChild(secondLevelElement12);
            //将第一层的第一个子节点加入到根节点下
            rootElement.AppendChild(firstLevelElement1);

            XmlElement firstLevelElement2 = XmlDoc.CreateElement("RseKey");

            XmlElement secondLevelElement21 = XmlDoc.CreateElement("PublicKey");
            secondLevelElement21.InnerText = "测试测试";
            XmlElement secondLevelElement22 = XmlDoc.CreateElement("PrivateKey");
            secondLevelElement22.InnerText = "测试测试";
            firstLevelElement2.AppendChild(secondLevelElement21);
            firstLevelElement2.AppendChild(secondLevelElement22);
            rootElement.AppendChild(firstLevelElement2);
            //将xml文件保存到指定的路径下
            FileUtil.CreateDirectory(XmlSavePath);
            XmlDoc.Save(Path.Combine(XmlSavePath, $"{FileName}.xml"));
            return Task.FromResult("创建成功");

        }

        /// <summary>
        /// 修改xml节点
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public Task<string> EditXmlNode()
        {
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml", "节点.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlSavePath);
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;


            const string node = "PublicKey";

            //可优化为递归方式
            foreach (XmlNode item in xmlNodeList)
            {
                //第一层级
                if (item.Name == "AesKey")
                {
                    //第二层级 
                    foreach (XmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == node)
                        {
                            item[node].InnerText = "修改后的值阿三大苏打实打实";
                        }
                    }
                }
            }
            xmlDoc.Save(XmlSavePath);

            return Task.FromResult("修改成功");
        }

        /// <summary>
        /// 创建一个xml文件（属性）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<string> CreateXmlAttr()
        {
            string FileName = "属性";
            //xml文件存放目录
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml");
            //初始化一个xml实例
            XmlDocument XmlDoc = new XmlDocument();
            //创建xml的根节点
            XmlElement rootElement = XmlDoc.CreateElement("BaseFortunateNode");
            //将根节点加入到xml文件中（AppendChild）
            XmlDoc.AppendChild(rootElement);

            //可优化，使用循环遍历创建节点

            #region 第一个子节点
            //初始化第一层的第一个子节点
            XmlElement firstLevelElement1 = XmlDoc.CreateElement("AesKey");
            firstLevelElement1.SetAttribute("PublicKey", "PublicKeyPublicKeyPublicKeyPublicKeyPublicKeyPublicKeyPublicKey");
            firstLevelElement1.SetAttribute("PrivateKey", "PrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKey");
            //将第一层的第一个子节点加入到根节点下
            rootElement.AppendChild(firstLevelElement1);
            #endregion

            #region 第二个子节点
            //初始化第一层的第二个子节点
            XmlElement firstLevelElement2 = XmlDoc.CreateElement("RsaKey");
            firstLevelElement2.SetAttribute("PublicKey", "PublicKeyPublicKeyPublicKeyPublicKeyPublicKeyPublicKeyPublicKey");
            firstLevelElement2.SetAttribute("PrivateKey", "PrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKeyPrivateKey");
            //将第一层的第二个子节点加入到根节点下
            rootElement.AppendChild(firstLevelElement2);
            #endregion

            //将xml文件保存到指定的路径下
            FileUtil.CreateDirectory(XmlSavePath);
            XmlDoc.Save(Path.Combine(XmlSavePath, $"{FileName}.xml"));
            return Task.FromResult("创建成功");

        }

        /// <summary>
        /// 删除xml节点
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public Task<string> DeleteXmlNode()
        {
            Console.WriteLine(DateTime.Now);
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml", "节点.xml");
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(XmlSavePath); //加载XML文档
            //要匹配的XPath表达式(例如:"//节点名//子节点名
            XmlNode xmlNode = xmlDoc.SelectSingleNode("/BaseFortunateNode/AesKey");
            if (xmlNode != null)
            {
                //删除节点
                xmlNode.ParentNode.RemoveChild(xmlNode);
                return Task.FromResult("节点删除成功");
            }
            return Task.FromResult("节点不存在");
        }

        /// <summary>
        /// 删除xml节点属性
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public Task<string> DeleteAttribute()
        {
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml", "属性.xml");
            bool isSuccess = XmlUtil.RemoveNodeAttr(XmlSavePath, "BaseFortunateNode/AesKey", "PublicKey");
            if (isSuccess)
            {
                return Task.FromResult("节点属性删除成功");
            }
            else
            {
                throw new Exception("节点属性删除失败");
            }
        }

        /// <summary>
        /// 获取Xml节点属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<string> GetAttributeValue()
        {
            string XmlSavePath = Path.Combine(FileUtil.GetSystemDirectory, "Xml", "属性.xml");

            #region 方法一
            /*
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlSavePath);
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode("BaseFortunateNode/AesKey");
            return Task.FromResult(node.Attributes["PublicKey"].Value);
            */
            #endregion

            #region 方法二
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlSavePath);
            string value = XmlUtil.GetNodeAttr(xmlDoc, "//BaseFortunateNode/AesKey", "PublicKey");
            return Task.FromResult(value);
            #endregion

        }
    }
}

