using UnityEngine;
using UnityEngine.UI;

namespace GameManager
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private int id;
        private Button button;
        private GameManager gameManager;

        void Start()
        {
            button = GetComponent<Button>();
            gameManager = GetComponentInParent<GameManager>();
            button.onClick.AddListener(SetDifficulty);
        }

        private void SetDifficulty()
        {
            gameManager.Menu(id);
        }
    }
}
