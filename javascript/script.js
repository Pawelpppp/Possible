window.onload = () => {
	const form = document.querySelector('#search-form');
	const prevPage = document.querySelector('.previous');
	const nextPage = document.querySelector('.next');

	const hideEl = el => el.style.display = 'none';
	const showEl = el => el.style.display = 'block';

	hideEl(prevPage);
	hideEl(nextPage);
	let page = 1;

	if (form) {
		form.addEventListener('submit', e => {
			e.preventDefault();
			page = 1;
			const data = getResponse(page);
			getData(data);
			showEl(nextPage);
		})
	}

	if(prevPage) {
		prevPage.addEventListener('click', e => {
			e.preventDefault();
			page -= 1;
			if (page > 0) {
				const data = getResponse(page);
				getData(data);

				if(page<=1)
				{
					console.log(page)
					hideEl(prevPage);
				}
			}
		})
	}

	if(nextPage) {
		nextPage.addEventListener('click', e => {
			e.preventDefault();
			page += 1
			const data = getResponse(page);
			getData(data);

			if(page > 1) {
				showEl(prevPage);
			}
		})
	}

	const getResponse = page => {
		const queryInput = document.querySelector('#search-text');
		const query = queryInput ? queryInput.value : '';
	
		return JSON.stringify({
			SearchText: query,
			Page: page
		});
	}
	
	const getData = async (data) => {
		const options = {
		  method: 'POST',
		  headers: {
			'Accept': 'application/json',
			"Content-Type": 'application/json; charset=utf-8'
		},
		  data,
		  url: 'http://localhost:59930/api/values'
		};
		const response = await axios(options);
		createResults(response.data);
	}
	
	const createResults = results => {
		const wrapper = document.querySelector('.twitter-feed');
		wrapper.innerText = '';
	
		results.forEach(result => {
			const tweet = document.createElement('div');
			tweet.className = 'tweet';
	
			const p = document.createElement('p');
			p.innerText = result.text;
	
			const tweetDatetime = document.createElement('p');
			tweetDatetime.className = 'tweet-datetime';
			tweetDatetime.innerText = `Posted by ${result.name} on ${result.date}`;
	
			tweet.appendChild(p);
			tweet.appendChild(tweetDatetime);
	
			wrapper.appendChild(tweet);
		});
	
		const resultsWrapper = document.querySelector('.results-container');
		resultsWrapper.style.display = 'block';
	}
}

