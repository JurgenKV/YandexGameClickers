
    using TMPro;
    using UnityEngine;

    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _textVariantFirst;
        [SerializeField] private TMP_Text _textVariantSecond;

        private QuestionItem _questionItem;
        
        public void CreateUI(QuestionItem questionItem)
        {
            _questionItem = questionItem;
            _text.text = questionItem.QuestionText;
            _textVariantFirst.text = questionItem.VariantFirst;
            _textVariantSecond.text = questionItem.VariantSecond;
        }

        public void ButtonFirst()
        {
            FindObjectOfType<GameController>().HandleButtonVariantFirst(_questionItem);
        }

        public void ButtonSecond()
        {
            FindObjectOfType<GameController>().HandleButtonVariantSecond(_questionItem);
        }
    }
