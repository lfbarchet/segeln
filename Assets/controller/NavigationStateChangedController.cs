using UnityEngine;
using PuzzleCubes.Models;

class NavigationStateChangedController : MonoBehaviour
{

    public Transform playerCube;

    private void OnEnable()
    {


        NavigationStateChangedEvent.Instance.AddListener(OnNavigationStateChanged);
    }

    private void OnDisable()
    {
        NavigationStateChangedEvent.Instance.RemoveListener(OnNavigationStateChanged);
    }

    private void OnNavigationStateChanged(NavigationState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Speed: {state.Speed}, Position: {state.Position}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation, 0);
        playerCube.position = new Vector3(state.Position.x, 0, state.Position.y);
    }
}