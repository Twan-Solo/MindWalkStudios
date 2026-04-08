//using StarterAssets;
////using Unity.Android.Gradle.Manifest;
//using UnityEngine;
//using UnityEngine.Windows;


//public class PlayerStickToWall : MonoBehaviour
//{
//    ThirdPersonController movementScript;
//    private StarterAssetsInputs _input;
//    public bool IsOnWall = false;
//    private void Start()
//    {
//        movementScript = GetComponent<ThirdPersonController>();
//        _input = GetComponent<StarterAssetsInputs>();
//    }
//    public void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("PaintedWall"))
//        {
//            movementScript.Gravity = 0;
//            movementScript._verticalVelocity = 0;
//            IsOnWall = true;
//            Debug.Log(IsOnWall);

//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("PaintedWall"))
//        {
//            movementScript.Gravity = -15;
//            IsOnWall=false;
//            Debug.Log(IsOnWall);
//        }
//    }
//} 
