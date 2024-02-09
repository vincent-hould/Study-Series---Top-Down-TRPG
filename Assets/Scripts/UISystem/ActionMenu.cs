using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownTRPG
{
    public class ActionMenu : MonoBehaviour, IActionMenu
    {
        [SerializeField] private GameObject ButtonPrefab;
        [SerializeField] private float DistanceFromOrigin = 50f;

        public void PromptForAction(Vector3 origin, List<ActionMenuItem> choices)
        {
            BuildButtons(choices);
            ShowMenu(origin);
        }

        private void BuildButtons(List<ActionMenuItem> choices)
        {
            var choiceIndex = 0;
            foreach (var choice in choices)
            {
                var gameObject = Instantiate(ButtonPrefab, transform);
                var button = gameObject.GetComponent<Button>();
                button.interactable = choice.Enabled;
                // TODO Might refactor into coroutine
                // see: https://discussions.unity.com/t/wait-for-button-response-in-coroutine/225851/3
                button.onClick.AddListener(() => {
                    HideMenu();
                    choice.Callback();
                });
                var text = gameObject.GetComponentInChildren<TMP_Text>();
                text.text = choice.Label;
                if (choiceIndex == 0)
                    button.Select();
                choiceIndex++;
            }
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
            DestroyButtons();
        }

        private void ShowMenu(Vector3 origin)
        {
            transform.position = GetMenuPosition(origin);
            gameObject.SetActive(true);
        }

        private void DestroyButtons()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        private Vector3 GetMenuPosition(Vector3 worldOrigin)
        {
            Camera camera = Helpers.Camera;
            Vector3 originScreenPoint = camera.WorldToScreenPoint(worldOrigin);
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
            originScreenPoint.x += originScreenPoint.x >= screenCenter.x ? -DistanceFromOrigin : DistanceFromOrigin;
            originScreenPoint.y += originScreenPoint.y >= screenCenter.y ? -DistanceFromOrigin : DistanceFromOrigin;
            return originScreenPoint;
        }
    }
}
