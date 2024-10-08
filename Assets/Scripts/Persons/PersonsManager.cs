using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonsManager : MonoBehaviour
{
    private List<Person> _persons = new List<Person>();

    public static event Action<Person> OnEnemyCollideWithPerson;
    public static event Action OnClickByPerson;

    private Person _selectedPerson;

    void Awake()
    {
        _persons = FindObjectsOfType<Person>().ToList();
    }

    void OnEnable()
    {
        EnemiesManager.OnEnemyCollision += OnEnemyCollisionHandler;
        Person.OnPersonClick += OnPersonClickHandler;
    }

    void OnDisable()
    {
        EnemiesManager.OnEnemyCollision -= OnEnemyCollisionHandler;
        Person.OnPersonClick -= OnPersonClickHandler;
    }

    private void OnEnemyCollisionHandler(Collider2D collision, Enemy enemy)
    {
        Person person = _persons.Find((obj) => obj.gameObject == collision.gameObject);

        if (person)
        {
            Destroy(person.gameObject);
            _persons.Remove(person);

            OnEnemyCollideWithPerson?.Invoke(person);
        };
    }

    private void OnPersonClickHandler(Person person)
    {
        if (_selectedPerson)
        {
            person.ClearSelection();
            _selectedPerson = null;
        }
        else
        {
            person.SetSelection();
            _selectedPerson = person;
        }

        OnClickByPerson?.Invoke();
    }

    public void MoveSelectedPersonTo(Vector2 vector)
    {
        if (_selectedPerson)
        {
            _selectedPerson.MoveTo(vector);

            _selectedPerson.ClearSelection();
            _selectedPerson = null;
        }
    }

    public List<Person> GetPersons()
    {
        return _persons;
    }

    public bool GetIsPersonSelected()
    {
        return !!_selectedPerson;
    }
}
