using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class UserStatisticPanel : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        public event Action<int> OnDrawDetailClick;
        public event Action<int> OnGuessDetailClick;
        public event Action<int> OnDeleteClick;

        [SerializeField]
        RectTransform _content;

        readonly Dictionary<int, GameObject> items = new();

        public void DestroyItem(int id)
        {
            Destroy(items[id]);
        }

        public async UniTaskVoid SetData(List<UserInfoDetail> userInfoDetails)
        {
            foreach (var userInfoDetail in userInfoDetails)
            {
                var userStatisticItem =
                    await UILoader.LoadPrefab(AssetManager.Instance.UserStatisticItemPrefab, _content);
                userStatisticItem.SetData(userInfoDetail.Name, userInfoDetail.Language, userInfoDetail.Sex,
                                          userInfoDetail.Age, userInfoDetail.Career, userInfoDetail.EducationLevel,
                                          userInfoDetail.DrawCount, userInfoDetail.GuessCount,
                                          userInfoDetail.GuessSuccessCount == 0
                                              ? 0
                                              : (float)userInfoDetail.GuessSuccessCount / userInfoDetail.GuessCount);
                userStatisticItem.OnDrawDetailClick  += () => OnDrawDetailClick?.Invoke(userInfoDetail.ID);
                userStatisticItem.OnGuessDetailClick += () => OnGuessDetailClick?.Invoke(userInfoDetail.ID);
                userStatisticItem.OnDeleteClick      += () => OnDeleteClick?.Invoke(userInfoDetail.ID);
                items.Add(userInfoDetail.ID, userStatisticItem.gameObject);
            }
        }
    }
}