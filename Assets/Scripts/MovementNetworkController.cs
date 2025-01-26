using Unity.Netcode;
using UnityEngine;


public class MovementNetworkController : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new();
    public float jumpForce = 5f;


    [Rpc(SendTo.Server)]
    void SubmitPositionRequestServerRpc(Vector3 position, RpcParams rpcParams = default) => Position.Value = position;


    void Update()
    {
        if (IsOwner && !IsServer)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");


            Vector3 movement = new Vector3(moveX, 0f, moveZ) * 5 * Time.deltaTime;
            transform.Translate(movement, Space.World);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            float rotationSpeed = 100f;
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            }

            SubmitPositionRequestServerRpc(transform.position);
        }


        if (IsServer)
            transform.position = Position.Value;
    }
}