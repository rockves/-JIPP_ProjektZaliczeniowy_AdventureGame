using System;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    /// <summary>
    /// Interfejs zawierający umiejętności z których każda ZywaIstota może korzystać.
    /// </summary>
    interface ZdolnosciIstot
    {
        /// <summary>
        /// Atak na inną istote zmodyfikowany o wszelkie bonusy do ataku.
        /// </summary>
        /// <param name="istotaAtakowana">Istota która będzie atakowana.</param>
        /// <returns>Obrażenia zadane.</returns>
        public float Atak(ZywaIstota istotaAtakowana);
        /// <summary>
        /// Obrona przed atakiem, zmniejsza życie o obrażenia zmodyfikowane przez odporności.
        /// </summary>
        /// <param name="obrazenia">Ilość obrażeń.</param>
        /// <returns>Obrażenia otrzymane.</returns>
        public float Obrona(float obrazenia);
        public bool CzyZyje();
        public void Ulecz();
        public void Ulecz(float ileUleczyc);
    }
}
