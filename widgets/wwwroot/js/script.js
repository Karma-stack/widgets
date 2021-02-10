const ratings = document.querySelectorAll('.rating');

if (window.localStorage.getItem('isRating')) {
    document.getElementById("rating").style.display = "none";
    document.getElementById("mapLinks").style.display = "none";
    document.getElementById("email-form").style.display = "none";
    document.getElementById("congrats").style.display = "";
}

console.log(localStorage.getItem('isRating'))

if (ratings.length > 0) {
    initRatings();
}


document.querySelector("form").addEventListener("submit", (e) => {
    document.getElementById("email-form").style.display = "none";
    document.getElementById("congrats").style.display = "";
    window.localStorage.setItem('isRating', 'true');
})

function initRatings() {
    let ratingActive, ratingValue;
    for (let i = 0; i < ratings.length; i++) {
        const rating = ratings[i];
        initRating(rating);
    }

    function initRating(rating) {
        initRatingVars(rating);

        setRatingActiveWidth();

        if (rating.classList.contains('rating_set')) {
            setRating(rating);
        }
    }

    function initRatingVars(rating) {
        ratingActive = rating.querySelector('.rating_active');
        ratingValue = rating.querySelector('.rating_value');
    }

    function setRatingActiveWidth(index = ratingValue.innerHTML) {
        const ratingActiveWidth = index / 0.05;
        ratingActive.style.width = `${ratingActiveWidth}%`;
    }

    function setRating(rating) {
        const ratingItems = document.querySelectorAll('.rating_item');
        for (let i = 0; i < ratingItems.length; i++) {
            const ratingItem = ratingItems[i];
            ratingItem.addEventListener('mouseenter', (e) => {
                initRatingVars(rating);
                setRatingActiveWidth(ratingItem.value);
            })
            ratingItem.addEventListener('mouseleave', (e) => {
                setRatingActiveWidth();
            })
            ratingItem.addEventListener('click', (e) => {
                initRatingVars(rating);
                ratingValue.innerHTML = ratingItem.value;
                setRatingActiveWidth(ratingItem.value);
                if (ratingValue.innerText == 5) {
                    //document.location.replace(`https://localhost:5001/Widgets/Review${document.location.search}`);
                    document.getElementById("rating").style.display = "none";
                    document.getElementById("mapLinks").style.display = "";
                }
                else {
                    //document.location.replace(`https://localhost:5001/Widgets/Email${document.location.search}`);
                    document.getElementById("rating").style.display = "none";
                    document.getElementById("email-form").style.display = "";
                }
            })
        }
    }



}
