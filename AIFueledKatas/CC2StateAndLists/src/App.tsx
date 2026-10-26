import { useState } from 'react'
import type { FormEvent } from 'react'

type Todo = {
  id: string
  text: string
  completed: boolean
}

type TodoItemProps = {
  todo: Todo
  onToggle: (id: string) => void
  onRemove: (id: string) => void
}

function TodoItem({ todo, onToggle, onRemove }: TodoItemProps) {
  return (
    <li>
      <label className={todo.completed ? 'completed' : undefined}>
        <input
          type="checkbox"
          checked={todo.completed}
          onChange={() => onToggle(todo.id)}
        />
        <span>{todo.text}</span>
      </label>
      <button
        type="button"
        className="secondary"
        aria-label={`Remove ${todo.text}`}
        onClick={() => onRemove(todo.id)}
      >
        Remove
      </button>
    </li>
  )
}

function App() {
  const [todos, setTodos] = useState<Todo[]>([
    { id: 'learn-state', text: 'Learn useState', completed: false },
    { id: 'render-list', text: 'Render a list with stable keys', completed: false },
  ])
  const [draft, setDraft] = useState('')
  const remaining = todos.filter((todo) => !todo.completed).length

  function addTodo(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    const text = draft.trim()
    if (!text) {
      return
    }

    const todo: Todo = { id: crypto.randomUUID(), text, completed: false }
    setTodos((current) => [...current, todo])
    setDraft('')
  }

  function toggleTodo(id: string) {
    setTodos((current) =>
      current.map((todo) =>
        todo.id === id ? { ...todo, completed: !todo.completed } : todo,
      ),
    )
  }

  function removeTodo(id: string) {
    setTodos((current) => current.filter((todo) => todo.id !== id))
  }

  function reverseIncorrectly() {
    // Intentionally broken: the same array reference lets React skip rendering.
    todos.reverse()
    setTodos(todos)
  }

  function reverseCorrectly() {
    setTodos((current) => current.toReversed())
  }

  return (
    <main>
      <header>
        <p className="eyebrow">Clean Code / Problem 2</p>
        <h1>State and lists</h1>
        <p>A small todo list to explore how React updates the screen.</p>
      </header>

      <form onSubmit={addTodo}>
        <label htmlFor="new-todo">New todo</label>
        <div className="input-row">
          <input
            id="new-todo"
            value={draft}
            onChange={(event) => setDraft(event.target.value)}
            placeholder="What do you want to learn?"
            maxLength={200}
            required
          />
          <button type="submit" disabled={!draft.trim()}>Add todo</button>
        </div>
      </form>

      <p role="status">{remaining} of {todos.length} remaining</p>
      {todos.length === 0 ? (
        <p>No todos yet. Add one above.</p>
      ) : (
        <ul>
          {todos.map((todo) => (
            <TodoItem
              key={todo.id}
              todo={todo}
              onToggle={toggleTodo}
              onRemove={removeTodo}
            />
          ))}
        </ul>
      )}

      <section aria-labelledby="experiment-title" className="experiment">
        <h2 id="experiment-title">Break it: mutate state directly</h2>
        <p>
          Start with at least two different todos. Click the broken reverse
          button first: the array changes in memory, but the screen stays the
          same because React receives the same array reference.
        </p>
        <p>
          Then type in the input to trigger another render and reveal the
          mutation. The correct reverse button creates a new array, so the
          screen updates immediately.
        </p>
        <div className="actions">
          <button
            type="button"
            className="secondary"
            disabled={todos.length < 2}
            onClick={reverseIncorrectly}
          >
            Reverse in place (broken)
          </button>
          <button
            type="button"
            disabled={todos.length < 2}
            onClick={reverseCorrectly}
          >
            Reverse with a new array
          </button>
        </div>
      </section>
    </main>
  )
}

export default App
