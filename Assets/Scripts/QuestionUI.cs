
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = UnityEngine.Random;

    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _textVariantFirst;
        [SerializeField] private TMP_Text _textVariantSecond;
        [SerializeField] private List<GameObject> _bgVariants;
        
        //[SerializeField] private GameController _controller;
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;

        
        private QuestionItem _questionItem;

        private void Awake()
        {
            RoleBgVariant();
        }
        
        public void CreateUI(QuestionItem questionItem)
        {
            _questionItem = questionItem;
            _text.text = questionItem.QuestionText;
            _textVariantFirst.text = questionItem.VariantFirst;
            _textVariantSecond.text = questionItem.VariantSecond;
        }

        public void ButtonFirst()
        {
            //_controller.HandleButtonVariantFirst(_questionItem);
            RoleBgVariant();
            StartCoroutine(ChillTimer());
        }

        public void ButtonSecond()
        {
            //_controller.HandleButtonVariantSecond(_questionItem);
            RoleBgVariant();
            StartCoroutine(ChillTimer());
        }

        private void RoleBgVariant()
        {
            _bgVariants.ForEach(i=>i.SetActive(false));
            _bgVariants[Random.Range(0, _bgVariants.Count)].SetActive(true);
        }

        private IEnumerator ChillTimer()
        {
            _button1.interactable = false;
            _button2.interactable = false;
            yield return new WaitForSeconds(1f);
            _button1.interactable = true;
            _button2.interactable = true;
            
        }
    }
