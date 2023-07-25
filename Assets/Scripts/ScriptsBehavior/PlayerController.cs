using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * В этом скрипте будет реализована оснавная механика, вызов методов для преобразование в движение
 * move (WSAD) - готово
 * Jump (space) - готово
 * FastRunning (leftShift) - готово
 * Поворот камеры и игрока  - готово
 * Выбор оружия (1,2,3)
 */


public class PlayerController : MonoBehaviour
{
    // WASD movePlayer()
    public float horizontal; //Переменные для работы с анимациями
    public float vertical; //Переменные для работы с анимациями
    private float speedWalk = 5;  //скорость ходьбы
    private float speedRun = 11;  //скорость ходьбы

    //space jumpPlayer()
    private float jumpForce = 6;  //сила прыжка
    public bool isJumping;
    private Vector3 Velocity;


    // mouseRotation()
    private float mouseX;
    private float mouseY;
    private float sensitivity = 2; //скорость мыши
    [SerializeField] public Vector2 mousePos; ////позиция для анимаций

    //плавность мыши
    private float curMouseX;
    private float curMouseY;
    private float currentVelX;
    private float currentVelY;
    private float smoothTime = 0.5f;

    public CharacterController playerControl;
    private GunInventary controlItem;
    private Camera playersCamera;
    private Camera gunCamera;



    private void Start()
    {
        playerControl = GetComponent<CharacterController>();
        controlItem = GetComponentInChildren<GunInventary>();
        playersCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        controlItem.UseWeapon(0);
    }

    private void Update()
    {
        //kis.TakeDamage(1);
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            movePlayer();
            chooseWeapon();

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse X") != 0)
                mouseRotation();
        }  
    }
    private void movePlayer()
    {
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? speedRun : speedWalk; //Пока зажат shift, скорость будет увеличена, иначе нет

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (playerControl.isGrounded)
        {
            Velocity.y = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                Velocity.y = jumpForce;
            }
        }
        else
        {
            Velocity.y -= 9.8f * 2 * Time.deltaTime;
        }

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical) * speed;
        moveDirection = transform.TransformDirection(moveDirection); //локальное движение осей кординат переводим в глобальное

        playerControl.Move(moveDirection * Time.deltaTime);
        playerControl.Move(Velocity * Time.deltaTime);
    }
    private void mouseRotation()
    {
        //Считываем перемещение мыши и умножаем на скорость мыши

        mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseX += mousePos.x * sensitivity;
        mouseY += mousePos.y * sensitivity;

        mouseY = Mathf.Clamp(mouseY, -50, 50);

        curMouseX = Mathf.SmoothDamp(mouseX, curMouseX, ref currentVelX, smoothTime);
        curMouseY = Mathf.SmoothDamp(mouseY, curMouseY, ref currentVelY, smoothTime);


        playersCamera.transform.rotation = Quaternion.Euler(-curMouseY, curMouseX, 0);
        // gunCamera.transform.rotation = Quaternion.Euler(-curMouseY, curMouseX, 0);
        transform.rotation = Quaternion.Euler(0, curMouseX, 0);

        if (mouseX > 360 || mouseX < -360)
        {
            mouseX = 0;
        }

    }
    private void chooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            controlItem.UseWeapon(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            controlItem.UseWeapon(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            controlItem.UseWeapon(2);
    }
}

//Можно было сделать и через не characterconroller
/* private void movePlayer()
 {
     //Прыжок игрока не больше 1 раза
     if (Input.GetKeyDown(KeyCode.Space) && countOfJumps < 1)
     {
         playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
         countOfJumps++;
     }

     //Пока зажат shift, скорость будет увеличина
     if (Input.GetKey(KeyCode.LeftShift))
     {
         speedWalk = 1000;
         Debug.Log("Shift is pressed");
     }
     //В ином случае возвращаем стандарстное значение
     else
     {
         speedWalk = 350;
     }

     //Принимаем значения для передвижения и умножаем на скорость 
     float forceHorizontal = Input.GetAxis("Horizontal") * speedWalk * Time.deltaTime;
     float forceVertical = Input.GetAxis("Vertical") * speedWalk * Time.deltaTime;

     //Проверяем было ли нажаты кнопки перемещения
     if (forceHorizontal != 0)
         playerRb.AddRelativeForce(Vector3.right * forceHorizontal);

     if (forceVertical != 0)
         playerRb.AddRelativeForce(Vector3.forward * forceVertical);
 }
*/  