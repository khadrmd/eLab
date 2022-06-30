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

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.StartsWith("GRANTED"));
            }
        }

        [UnityTest]
        public IEnumerator Register()
        {
            WWWForm form = new WWWForm();

            form.AddField("fullname", "Test Name");
            form.AddField("email", "testemail@gmail.com");
            form.AddField("password", "testpassword");
            form.AddField("cpassword", "testcpassword");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "register.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.StartsWith("GRANTED"));
            }
        }

        [UnityTest]
        public IEnumerator SearchArchiveByKeyword()
        {
            WWWForm form = new WWWForm();

            form.AddField("keyword", "Title");
            form.AddField("date", "");
            form.AddField("filter", "");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "read_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.Contains("|"));
            }
        }

        [UnityTest]
        public IEnumerator SearchArchiveByFilter()
        {
            WWWForm form = new WWWForm();

            form.AddField("keyword", "");
            form.AddField("date", "");
            form.AddField("filter", "Events");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "read_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.Contains("|"));
            }
        }

        [UnityTest]
        public IEnumerator SearchArchiveByDate()
        {
            WWWForm form = new WWWForm();

            form.AddField("keyword", "");
            form.AddField("date", "2020-06-01");
            form.AddField("filter", "");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "read_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.Contains("|"));
            }
        }

        [UnityTest]
        public IEnumerator CreateArchive()
        {
            WWWForm form = new WWWForm();

            form.AddField("title", "Example Title");
            form.AddField("desc", "Example Description");
            form.AddField("type", "Achievements");
            form.AddField("date", "2022-06-24");
            form.AddField("author", "Husein");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "create_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.StartsWith("SUCCESS"));
            }
        }

        [UnityTest]
        public IEnumerator EditArchive()
        {
            WWWForm form = new WWWForm();

            form.AddField("id", "21");
            form.AddField("title", "Example Edit Title");
            form.AddField("desc", "Example Edit Description");
            form.AddField("date", "2022-06-24");
            form.AddField("type", "Events");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "update_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.StartsWith("SUCCESS"));
            }
        }

        [UnityTest]
        public IEnumerator DeleteArchive()
        {
            WWWForm form = new WWWForm();

            form.AddField("id", "24");

            using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "delete_archive.php", form))
            {
                yield return www.SendWebRequest();

                Assert.IsFalse(www.isHttpError);
                string responseText = www.downloadHandler.text;
                Assert.IsTrue(responseText.StartsWith("SUCCESS"));
            }
        }
    }
}
