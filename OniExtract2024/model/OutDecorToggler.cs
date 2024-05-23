
namespace OniExtract2024
{
    public class OutDecorToggler
    {
        public int posDecor;
        public int posRadius;
        public int negDecor;
        public int negRadius;

        public OutDecorToggler(EvilFlower evilFlower)
        {
            this.posDecor = evilFlower.positive_decor_effect.amount;
            this.posRadius = evilFlower.positive_decor_effect.radius;
            this.negDecor = evilFlower.negative_decor_effect.amount;
            this.negRadius = evilFlower.negative_decor_effect.radius;
        }

        public OutDecorToggler(PrickleGrass prickleGrass)
        {
            this.posDecor = prickleGrass.positive_decor_effect.amount;
            this.posRadius = prickleGrass.positive_decor_effect.radius;
            this.negDecor = prickleGrass.negative_decor_effect.amount;
            this.negRadius = prickleGrass.negative_decor_effect.radius;
        }

    }
}