using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Networking;

namespace Tests
{
    public class AuthTests
    {
        const string rootUrl = "http://localhost/elab/";
        [UnityTest]
        public IEnumerator Login()
        {
            WWWForm form = new WWWForm();

            form.AddField("email", "husein@gmail.com");
            form.AddField("password", "123456");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "login.php", form))
            {
                yield return www.SendWebRequest();

                Assert.AreEqual(false, www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.AreEqual(true, www.downloadHandler.text.StartsWith("GRANTED"));
            }
        }
    }
}
