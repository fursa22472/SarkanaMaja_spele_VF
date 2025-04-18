using UnityEngine;

public class MusicProximityDampener : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float minVolumeMultiplier = 0.3f;

    private Transform player;
    private Collider npcCollider;
    private Collider playerCollider;

    private static float closestVolume = 1f;

    void OnEnable()
    {
        TryFindPlayer();
        npcCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (player == null) TryFindPlayer();
        if (player == null || PersistentMusicManager.Instance == null) return;

        if (npcCollider != null && playerCollider != null)
        {
            // Use collider-based closest point distance
            Vector3 npcPoint = npcCollider.ClosestPoint(player.position);
            Vector3 playerPoint = playerCollider.ClosestPoint(npcPoint);
            float distance = Vector3.Distance(npcPoint, playerPoint);

            float t = Mathf.Clamp01(distance / detectionRadius);
            float thisVolume = Mathf.Lerp(minVolumeMultiplier, 1f, t);

            if (thisVolume < closestVolume)
            {
                closestVolume = thisVolume;
                PersistentMusicManager.Instance.SetVolumeMultiplier(thisVolume);
            }
        }
        else
        {
            // Fallback to transform distance
            float distance = Vector3.Distance(transform.position, player.position);
            float t = Mathf.Clamp01(distance / detectionRadius);
            float thisVolume = Mathf.Lerp(minVolumeMultiplier, 1f, t);

            if (thisVolume < closestVolume)
            {
                closestVolume = thisVolume;
                PersistentMusicManager.Instance.SetVolumeMultiplier(thisVolume);
            }
        }
    }

    void LateUpdate()
    {
        closestVolume = 1f;
    }

    void TryFindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerCollider = playerObj.GetComponent<Collider>(); // 👈 Grab player collider
        }
    }
}
