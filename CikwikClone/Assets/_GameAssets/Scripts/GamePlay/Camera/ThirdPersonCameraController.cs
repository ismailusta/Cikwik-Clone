using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _playerVisualTransform;
    [SerializeField] private Transform _orientationTransform;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play &&
        GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized;

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = _orientationTransform.forward * inputVertical + _orientationTransform.right * inputHorizontal;
        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }

    }

}
