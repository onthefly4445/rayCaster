using UnityEngine;
using System.Collections;


public class RaycastShooter : MonoBehaviour
{
    public int gunDmg = 3;
    public float fireRate = 0.1f;
    public float gunRange = 50f;
    public float gunForce = 100.0f;
    public Transform gunEnd;

    public Camera fpsCamera;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    public float nextFire;
    void Start() {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            
            Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            laserLine.SetPosition(0, gunEnd.position);
        
            if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out RaycastHit hit, gunRange)) {
                laserLine.SetPosition(1, hit.point);
                ShootableBox box = hit.collider.GetComponent<ShootableBox>();
                if (box != null){
                    print(box);
                    box.Damage(gunDmg);
                }
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * gunForce);
                }
            }
            else {
                laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * gunRange));
            }

        }
    }

    private IEnumerator ShotEffect() {
        gunAudio.Play();
        laserLine.enabled = true;
        
        yield return shotDuration;
        
        laserLine.enabled = false;
    }
}
