import React from 'react';
import RecipeTitle from './RecipeTitle';
import './index'

function App() {
    return (
        <article>
            <h1>Recipe Manager</h1>
            <div>{ Date.now() }</div>
            <RecipeTitle />
        </article>
    )
}

export default App;