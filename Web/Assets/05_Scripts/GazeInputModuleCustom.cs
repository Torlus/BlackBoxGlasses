using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class GazeInputModuleCustom : BaseInputModule{
	// The pixel through which to cast rays, in viewport coordinates.  Generally, the center
	// pixel is best, assuming a monoscopic camera is selected as the Canvas' event camera.
	[HideInInspector]
	public Vector2 hotspot = new Vector2(0.5f, 0.5f);
	[Tooltip("Optional object to place at raycast intersections as a 3D cursor. " +
	         "Be sure it is on a layer that raycasts will ignore.")]
	public GameObject cursor;
	private PointerEventData pointerData;


	private void CastRayFromGaze() {
		if (pointerData == null) {
			pointerData = new PointerEventData(eventSystem);
		}
		pointerData.Reset();
		pointerData.position = new Vector2(hotspot.x * Screen.width, hotspot.y * Screen.height);
		eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
		pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
		m_RaycastResultCache.Clear();
	}

	
	private void UpdateCurrentObject() {
		// Send enter events and update the highlight.
		var go = pointerData.pointerCurrentRaycast.gameObject;

		HandlePointerExitAndEnter(pointerData, go);
		// Update the current selection, or clear if it is no longer the current object.
		var selected = ExecuteEvents.GetEventHandler<ISelectHandler>(go);
		if (selected == eventSystem.currentSelectedGameObject) {
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, GetBaseEventData(),
			                      ExecuteEvents.updateSelectedHandler);
		}
		else {
			eventSystem.SetSelectedGameObject(null, pointerData);
		}
	}
	public override bool IsPointerOverGameObject(int pointerId) {
		return pointerData != null && pointerData.pointerEnter != null;
	}

	public override void Process() {
		CastRayFromGaze();
		UpdateCurrentObject();
		PlaceCursor();
	
	
	}
	public override void DeactivateModule() {
		base.DeactivateModule();
		if (pointerData != null) {
		
			HandlePointerExitAndEnter(pointerData, null);
			pointerData = null;
		}
		eventSystem.SetSelectedGameObject(null, GetBaseEventData());
		if (cursor != null) {
			cursor.SetActive(false);
		}
	}

	private void PlaceCursor() {
		if (cursor == null)
			return;
		var go = pointerData.pointerCurrentRaycast.gameObject;
		cursor.SetActive(go != null);

		if (cursor.activeInHierarchy) {
			Camera cam = pointerData.enterEventCamera;
			// Note: rays through screen start at near clipping plane.
			float dist = pointerData.pointerCurrentRaycast.distance + cam.nearClipPlane;
			cursor.transform.position = cam.transform.position + cam.transform.forward * dist;
		}
	}
}
