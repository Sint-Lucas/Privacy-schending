using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameObject[] adPrefabs;
    public Transform adContainer;
    public RectTransform canvasRectTransform;
    public GameObject panel;
    public GameObject panel2;

    private GameObject soundManager;
    private AudioSource SFX;
    public AudioClip twitterSFX;
    public AudioClip WindowsError;

    public float spawnTime = 0.64f;
    public int ads = 0;
    public int maxActiveAds = 8;

    private float cdTime = 1f;
    private float cdTimer;
    private float timeLeft = 30f;

    public TMP_Text Text;

    private bool isStarting;

    private void Awake()
    {
        isStarting = false;
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Start()
    {

        InvokeRepeating("SpawnAd", 0f, spawnTime);

        GameObject canvas = GameObject.Find("Canvas");
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        soundManager = GameObject.Find("SoundManager");
        SFX = soundManager.GetComponent<AudioSource>();

    }

    void SpawnAd()
    {
        
        int activeAdCount = adContainer.childCount;


        if (activeAdCount < maxActiveAds && isStarting == true)
        {
           
            int randomIndex = Random.Range(0, adPrefabs.Length);
            GameObject selectedAdPrefab = adPrefabs[randomIndex];

            SFX.PlayOneShot(twitterSFX);

            GameObject newAd = Instantiate(selectedAdPrefab, adContainer);
           
            float xPos = Random.Range(-canvasRectTransform.rect.width / 3, canvasRectTransform.rect.width / 3);
            float yPos = Random.Range(-canvasRectTransform.rect.height / 3, canvasRectTransform.rect.height / 3);

            
            newAd.transform.localPosition = new Vector3(xPos, yPos, 0);
            
            Button button = newAd.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnAdClick(newAd));
            }
        }
    }

    private void Update()
    {
        if (isStarting)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer > cdTime)
            {
                timeLeft--;
                cdTimer = 0;
                Debug.Log(timeLeft);
            }

            if (timeLeft <= 0)
            {
                isStarting = false;
                panel2.SetActive(true);
                Text.text = $"Tijd is om! Aantal keren je privacy beschermt: {ads}";
                Time.timeScale = 0;
            }
        }
    }

    public void OnAdClick(GameObject ad)
    {
        SFX.PlayOneShot(WindowsError);
        Destroy(ad);
        ads++;
    }

    public void StartGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        isStarting = true;
    }
}