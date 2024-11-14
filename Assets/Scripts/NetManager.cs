using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Painter.Dto;
using PunctualSolutions.Tool.Singleton;
using PunctualSolutionsTool.Tool;
using SimplePainterServer.Dto;
using UnityEngine;
using UnityEngine.Networking;

namespace Painter
{
    [SingletonMono]
    public partial class NetManager : MonoBehaviour
    {
        const        string Url        = "http://119.3.163.38:5001";
        public const string ADMIN_NAME = "Admin";

        public async UniTask<UserInfoDto> Login(UserInfoDto userInfo)
        {
            var unityWebRequest = UnityWebRequest.Put($"{Url}/UserInfo", userInfo.ToJson());
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");
            var value = await unityWebRequest.SendWebRequest();
            if (value.responseCode != 200) Debug.LogError($"{nameof(Login)} send error");
            return value.ToReturn<UserInfoDto>();
        }

        public async UniTask<UserInfoDto> Login(string userName)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/UserInfo/OrUserName?userName={userName}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode == 404) return null;
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(Login)} send error");
            return unityWebRequest.ToReturn<UserInfoDto>();
        }

        public async UniTask Test()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Word/AddAllWord");
            var value           = await unityWebRequest.SendWebRequest();
            if (value.responseCode != 200) Debug.LogError($"{nameof(Login)} send error");
        }

        public async UniTask<List<WordInfoDto>> GetAllWords()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Word/List");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetAllWords)} send error");
            return unityWebRequest.ToReturn<List<WordInfoDto>>();
        }

        public async UniTask PostImage(ImageInfoDto imageInfo, Texture2D texture)
        {
            var unityWebRequest = UnityWebRequest.Post($"{Url}/Image", imageInfo.ToJson(), "application/json");
            var value           = await unityWebRequest.SendWebRequest();
            if (value.responseCode != 200) Debug.LogError($"{nameof(PostImage)} send error");
            var id = unityWebRequest.ToReturn<ImageInfoDto>().ID;

            var bytes = texture.EncodeToPNG();
            var form  = new WWWForm();
            form.AddBinaryData("file", bytes, "image.png", "image/png");
            var www = UnityWebRequest.Post($"{Url}/Image/File?id={id}", form);
            await www.SendWebRequest();
            if (www.responseCode != 200) Debug.LogError($"{nameof(PostImage)} file send error");
        }

        public async UniTask<List<ImageInfoDto>> GetImageList()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Image/List");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetImageList)} send error");
            return unityWebRequest.ToReturn<List<ImageInfoDto>>();
        }

        public async UniTask<Texture2D> GetImageTexture(int id)
        {
            var www = UnityWebRequestTexture.GetTexture($"{Url}/Image/File?id={id}");
            await www.SendWebRequest();
            if (www.responseCode != 200) Debug.LogError($"{nameof(GetImageTexture)} send error");
            return DownloadHandlerTexture.GetContent(www);
        }

        public async UniTask<string[]> GetImageTextureTipWords(int id)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Image/TipWords?id={id}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetImageTextureTipWords)} send error");
            return unityWebRequest.ToReturn<string[]>();
        }

        public async UniTask<GuessDto> PostGuess(GuessDto guessDto)
        {
            var unityWebRequest = UnityWebRequest.Post($"{Url}/Guess", guessDto.ToJson(), "application/json");
            var value           = await unityWebRequest.SendWebRequest();
            if (value.responseCode != 200) Debug.LogError($"{nameof(PostGuess)} send error");
            return value.ToReturn<GuessDto>();
        }

        public async UniTask<float> DrawGuessCompletion()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/UserInfo/DrawGuessCompletion");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(DrawGuessCompletion)} send error");
            return unityWebRequest.ToReturn<float>();
        }

        public async UniTask<List<GuessDto>> GetGuessListForImage(int imageId)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Guess/ListForImage?imageId={imageId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetGuessListForImage)} send error");
            return unityWebRequest.ToReturn<List<GuessDto>>();
        }

        public async UniTask<List<GuessDetail>> GetGuessListForUser(int userId)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Guess/ListForUser?userId={userId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetGuessListForUser)} send error");
            return unityWebRequest.ToReturn<List<GuessDetail>>();
        }

        public async UniTask<List<ImageInfoDetail>> GetImageInfoListForWord(int wordId)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Image/ListForWord?wordId={wordId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetImageInfoListForWord)} send error");
            return unityWebRequest.ToReturn<List<ImageInfoDetail>>();
        }

        public async UniTask<List<ImageInfoDetail>> GetImageInfoListForUser(int userId)
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Image/ListForUser?userId={userId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetImageInfoListForUser)} send error");
            return unityWebRequest.ToReturn<List<ImageInfoDetail>>();
        }

        public async UniTask<List<UserInfoDetail>> GetUserInfoDetailList()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/UserInfo/ListForDetail");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetUserInfoDetailList)} send error");
            return unityWebRequest.ToReturn<List<UserInfoDetail>>();
        }

        public async UniTask<List<WordInfoDetail>> GetWordInfoDetailList()
        {
            var unityWebRequest = UnityWebRequest.Get($"{Url}/Word/DetailList");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(GetWordInfoDetailList)} send error");
            return unityWebRequest.ToReturn<List<WordInfoDetail>>();
        }

        public async UniTask DeleteUser(int userId)
        {
            var unityWebRequest = UnityWebRequest.Delete($"{Url}/UserInfo?id={userId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(DeleteUser)} send error");
        }

        public async UniTask ClearWord(int wordId)
        {
            var unityWebRequest = UnityWebRequest.Delete($"{Url}/Word/ClearData?wordId={wordId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(ClearWord)} send error");
        }

        public async UniTask DeleteWord(int wordId)
        {
            var unityWebRequest = UnityWebRequest.Delete($"{Url}/Word?id={wordId}");
            await unityWebRequest.SendWebRequest();
            if (unityWebRequest.responseCode != 200) Debug.LogError($"{nameof(DeleteWord)} send error");
        }

        public async UniTask AddWord(string word, string partOfSpeech)
        {
            var wordInfo        = new WordInfoDto(0, word, partOfSpeech);
            var unityWebRequest = UnityWebRequest.Post($"{Url}/Word", wordInfo.ToJson(), "application/json");
            var value           = await unityWebRequest.SendWebRequest();
            if (value.responseCode != 200) Debug.LogError($"{nameof(AddWord)} send error");
        }

        public void OnSingletonInit()
        {
        }

        public void Dispose()
        {
        }

        static void InAwake()
        {
        }
    }

    public static class WebRequestTool
    {
        /// <summary>
        /// TODO 放到PS TOOL里面
        /// </summary>
        public static T ToReturn<T>(this UnityWebRequest unityWebRequest) =>
            unityWebRequest.downloadHandler.text.JsonToObject<T>();
    }
}