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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
            {
                HideMenu();
                UIEventChannelSO.RaiseActionMenuCancelled();
            }
        }

        public void PromptForAction(Vector3 origin, List<ActionMenuItem> choices)
        {
            // It's important that the menu (parent game object) is activated before the buttons are created
            // or else the call of button.Select() won't be propagated up the hierarchy to the event system
            ShowMenu(origin);
            BuildButtons(choices);
        }

        private void BuildButtons(List<ActionMenuItem> choices)
        {
            var isFirstInteractable = true;
            foreach (var choice in choices)
            {
                var gameObject = Instantiate(ButtonPrefab, transform);
                var button = gameObject.GetComponent<Button>();
                button.onClick.AddListener(() => {
                    HideMenu();
                    choice.Callback();
                });
                button.interactable = choice.Enabled;
                if (choice.Enabled && isFirstInteractable)
                {
                    button.Select();
                    isFirstInteractable = false;
                }
                var text = gameObject.GetComponentInChildren<TMP_Text>();
                text.text = choice.Label;
                
            }
        }

        private void HideMenu()
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
