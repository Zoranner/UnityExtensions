//============================================================
// Project: DigitalFactory
// Author: KimoTech@KIMOTECH
// Datetime: 2019-04-09 20:46:27
// Description: TODO >> This is a script Description.
//============================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zoranner.Engine.Extensions
{
    public static class EventSystemExtensions
    {
        private static EventSystemHelper _Helper;

        public static bool IsOverGui(this EventSystem eventSystem)
        {
            if (!eventSystem)
            {
                return false;
            }

            if (_Helper == null)
            {
                _Helper = new EventSystemHelper();
            }

            if (_Helper.GraphicRaycasters == null)
            {
                return false;
            }

            return eventSystem.IsPointerOverGameObject() ||
                   _Helper.GraphicRaycasters.Any(eventSystem.CheckGuiRaycastObjects);
        }

        private static bool CheckGuiRaycastObjects(this EventSystem eventSystem, GraphicRaycaster graphicRaycaster)
        {
            var eventData = new PointerEventData(eventSystem)
            {
                pressPosition = Input.mousePosition,
                position = Input.mousePosition
            };

            var list = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, list);
            return list.Count > 0;
        }
    }

    public class EventSystemHelper/* : KimoBehaviour*/
    {
        public GraphicRaycaster[] GraphicRaycasters;

        //protected override void Start()
        //{
        //    GraphicRaycasters = Resources.FindObjectsOfTypeAll<GameObject>()
        //        .Where(standObject => !standObject.IsPrefab() && standObject.GetComponent<GraphicRaycaster>())
        //        .Select(standObject => standObject.GetComponent<GraphicRaycaster>()).ToArray();
        //    //Debug.Log($"GraphicRaycasters' length is {GraphicRaycasters.Length}");
        //}
    }
}