using System.Xml;

namespace AhCha.Fortunate.Common.Utility
{
    public class XmlUtil
    {

        /// <summary>
        /// 依据XML文件或XML格式字符串，获得XmlDocument对象。
        /// </summary>
        /// <param name="filePathOrXmlStr">XML文件路径（或XML格式字符串）</param>
        /// <returns>返回XmlDocument对象，出现异常时返回值为空</returns>
        public static XmlDocument GetXmlDoc(string filePathOrXmlStr)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (File.Exists(filePathOrXmlStr))
                {
                    xmlDoc.Load(filePathOrXmlStr);
                }
                else
                {
                    xmlDoc.LoadXml(filePathOrXmlStr);
                }
                return xmlDoc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 将XmlDocument对象保存为指定路径的XML文件。
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <returns>出现异常时返回值为False</returns>
        public static bool SaveXmlDoc(string filePath, XmlDocument xmlDoc)
        {
            try
            {
                xmlDoc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取XPATH指定节点的值。
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <param name="xpath">XPATH字符串参数，如：//CacheTime、//settings/CacheTime、...</param>
        /// <returns>出现异常时，返回值为空</returns>
        public static string GetNodeValue(XmlDocument xmlDoc, string xpath)
        {
            try
            {
                return xmlDoc.DocumentElement.SelectSingleNode(xpath).InnerText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置XPATH指定节点的值。
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <param name="xpath">XPATH值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="nodeVal">要设置的节点的值</param>
        /// <returns>出现异常时，返回值为False</returns>
        public static bool SetNodeValue(ref XmlDocument xmlDoc, string xpath, string nodeVal)
        {
            try
            {
                xmlDoc.DocumentElement.SelectSingleNode(xpath).InnerText = nodeVal;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取XPATH指定节点下，指定属性的值。
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <param name="nodePath">XPATH值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="attrName">要设置的节点属性名称</param>
        /// <returns></returns>
        public static string GetNodeAttr(XmlDocument xmlDoc, string nodePath, string attrName)
        {
            try
            {
                XmlNode node = xmlDoc.DocumentElement.SelectSingleNode(nodePath);
                return node.Attributes[attrName].Value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置XPATH指定节点下，指定属性的值。
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <param name="nodePath">XPATH值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="attrName">要设置的节点属性名称</param>
        /// <param name="attrVal">要设置的节点属性的值</param>
        /// <returns></returns>
        public static bool SetNodeAttr(ref XmlDocument xmlDoc, string nodePath, string attrName, string attrVal)
        {
            try
            {
                XmlNode node = xmlDoc.DocumentElement.SelectSingleNode(nodePath);
                ((XmlElement)node).SetAttribute(attrName, attrVal);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新XML文件的节点的值。
        /// <para>操作步骤：打开XML->更新节点->写入XML文件，因此，本方法不适用于频繁调用。</para>
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="xpath">XPATH值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="nodeVal">要设置的节点的值</param>
        /// <returns></returns>
        public static bool UpdateXmlFileNodeValue(string filePath, string xpath, string nodeVal)
        {
            XmlDocument xmlDoc = GetXmlDoc(filePath);
            if (SetNodeValue(ref xmlDoc, xpath, nodeVal))
                return SaveXmlDoc(filePath, xmlDoc);
            return false;
        }

        /// <summary>
        /// 更新XML文件的节点下指定属性的值。
        /// <para>操作步骤：打开XML->更新节点->写入XML文件，因此，本方法不适用于频繁调用。</para>
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">xpath值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="attrName">要设置的节点属性名称</param>
        /// <param name="attrVal">要设置的节点属性的值</param>
        /// <returns></returns>
        public static bool UpdateXmlFileNodeAttr(string filePath, string nodePath, string attrName, string attrVal)
        {
            XmlDocument xmlDoc = GetXmlDoc(filePath);
            if (SetNodeAttr(ref xmlDoc, nodePath, attrName, attrVal))
                return SaveXmlDoc(filePath, xmlDoc);
            return false;
        }

        /// <summary>
        /// 移除XPATH指定节点下，指定属性的值。
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <param name="nodePath">XPATH值，如：//CacheTime、//appSettings/CacheTime、...</param>
        /// <param name="attrName">要移除的节点属性名称</param>
        /// <param name="isWhere">是否移除所有属性</param>
        /// <returns></returns>
        public static bool RemoveNodeAttr(string XmlPath, string nodePath, string attrName, bool isWhere = false)
        {
            if (!File.Exists(XmlPath))
            {   //文件不存在
                return false;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlPath);
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(nodePath);

            if (isWhere)
            {
                //移除当前节点所有属性，不包括默认属性
                node.RemoveAllAttributes();
            }
            else
            {
                //移除指定属性
                node.RemoveAttribute(attrName);
            }
            xmlDoc.Save(XmlPath);
            return true;
        }
    
    }
}
