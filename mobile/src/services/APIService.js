import axios from 'axios';

function fetchGetSavedFilmsOfUser(userId, page) {
  return axios.get(
    `http://localhost:5133/api/Films/getSavedFilmsOfUser?UserId=${userId}&PageNumber=${page}`,
  );
}

function fetchSearchFilms(searchedFilmTitle, page) {
  return axios.get(
    `http://localhost:5133/api/Films/searchFilms?Name=${searchedFilmTitle}&Page=${page}`,
  );
}

function fetchSaveFilm(selectedSearchedFilmId, userId, status) {
  return axios.post('http://localhost:5133/api/Films/saveFilm', {
    filmId: selectedSearchedFilmId,
    userId: userId,
    status: status,
  });
}

export {fetchGetSavedFilmsOfUser, fetchSaveFilm, fetchSearchFilms};
