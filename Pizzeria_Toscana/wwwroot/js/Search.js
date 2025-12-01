async function fetchSuggestions(query) {
    const suggestionsDiv = document.getElementById('suggestions');
    suggestionsDiv.innerHTML = '';

    if (query.trim().length < 1) {
        suggestionsDiv.classList.remove('active');
        return;
    }

    try {
        const response = await fetch(`/Meniu/GetSearchSuggestions?term=${encodeURIComponent(query)}`);
        if (!response.ok) throw new Error('Network error');
        const suggestions = await response.json();

        if (suggestions.length === 0) {
            suggestionsDiv.classList.remove('active');
            return;
        }

        suggestions.forEach(suggestion => {
            const suggestionItem = document.createElement('div');
            suggestionItem.classList.add('suggestion-item');
            suggestionItem.textContent = suggestion;


            suggestionItem.onclick = () => {
                document.getElementById('searchQuery').value = suggestion;
                suggestionsDiv.classList.remove('active');
                document.getElementById('searchQuery').focus();
            };

            suggestionsDiv.appendChild(suggestionItem);
        });

        suggestionsDiv.classList.add('active');
    } catch (error) {
        console.error('Error fetching suggestions:', error);
        suggestionsDiv.classList.remove('active');
    }
}

document.addEventListener('click', (event) => {
    const searchBox = document.getElementById('searchQuery');
    const suggestionsDiv = document.getElementById('suggestions');

    if (!suggestionsDiv.contains(event.target) && event.target !== searchBox) {
        suggestionsDiv.classList.remove('active');
    }
});