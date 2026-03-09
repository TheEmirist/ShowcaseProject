using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Emirist.CharacterController
{
    [DefaultExecutionOrder(-2)]
    public class ThirdPersonInput : MonoBehaviour, PlayerControls.IThirdPersonMapActions
    {
        #region Class Variables
        public Vector2 ScrollInput { get; private set; }

        [SerializeField] private CinemachineCamera _virtualCamera;
        [SerializeField] private float _cameraZoomSpeed = 1.0f;
        [SerializeField] private float _cameraMinZoom = 1f;
        [SerializeField] private float _cameraMaxZoom = 5f;

        private CinemachineThirdPersonFollow _thirdPersonFollow;
        #endregion

        #region Startup
        private void Awake()
        {
            _thirdPersonFollow = _virtualCamera.GetComponent<CinemachineThirdPersonFollow>();
        }

        private void OnEnable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("PlayerInputManager instance not found. Please ensure there is a PlayerInputManager in the scene.");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Enable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("PlayerInputManager instance not found. Please ensure there is a PlayerInputManager in the scene.");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Disable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.RemoveCallbacks(this);
        }
        #endregion

        #region Callbacks
        public void OnScrollCamera(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            Vector2 scrollInput = context.ReadValue<Vector2>();
            // Normalize the scroll input to ensure consistent zoom speed regardless of scroll intensity
            // We also invert the scroll input to match typical zoom behavior (scrolling up zooms in, scrolling down zooms out)
            ScrollInput = scrollInput.normalized * _cameraZoomSpeed * -1f;
            print($"Scroll input: {scrollInput}");
        }

        #endregion

        #region Update
        private void Update()
        {
            _thirdPersonFollow.CameraDistance = Mathf.Clamp(_thirdPersonFollow.CameraDistance + ScrollInput.y, _cameraMinZoom, _cameraMaxZoom);
        }

        private void LateUpdate()
        {
            ScrollInput = Vector2.zero; // Reset scroll input after applying it to the camera
        }
        #endregion
    }
}