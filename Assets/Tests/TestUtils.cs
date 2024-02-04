using UnityEngine.SceneManagement; 
using System.Collections;

namespace Tests.Utils {
    public class Utils {

        public static IEnumerator WaitForLevelLoad(string level) {
            while (SceneManager.GetActiveScene().name != level) {
                yield return null;
            }

        }
    }
}

