import axios from 'axios';

function fetchGetSavedFilmsOfUser(userId, page, size, status, filmName) {
  let url = `http://localhost:5133/api/Films/getSavedFilmsOfUser?UserId=${userId}&Parameters.PaginationParameters.CurrentPage=${page}`;

  if (status !== undefined && status !== null) {
    url = url.concat(`&Parameters.Status=${status}`);
  }

  if (filmName && filmName.length !== 0) {
    url = url.concat(`&Parameters.SearchTerm=${filmName}`);
  }

  if (size !== null && size !== undefined) {
    url = url.concat(`&Parameters.PaginationParameters.PageSize=${size}`);
  }

  return axios.get(url);
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

function fetchGetUserInformations(id) {
  return axios.get(`http://localhost:5133/api/Users/${id}`);
}

function fetchPostsByUserId(userId, page) {
  return axios.get(
    `http://localhost:5133/api/Posts/getAllByUserId?UserId=${userId}&PageNumber=${page}`,
  );
}

function fetchCreatePost(formData) {
  return axios.post('http://localhost:5133/api/Posts/create', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
}

function fetchGetPostById(postId) {
  return axios.get(`http://localhost:5133/api/Posts/${postId}`);
}

function fetchGetCommentsPostById(postId, pageNumber = 1) {
  return axios.get(
    `http://localhost:5133/api/Comments/getAllByPostId?PostId=${postId}&PageNumber=${pageNumber}`,
  );
}

function fetchGetSubCommentsByParentCommentId(parentCommentId, pageNumber = 1) {
  return axios.get(
    `http://localhost:5133/api/Comments/getAllByParentCommentId?Id=${parentCommentId}&PageNumber=${pageNumber}`,
  );
}

export {
  fetchGetSubCommentsByParentCommentId,
  fetchGetCommentsPostById,
  fetchGetPostById,
  fetchCreatePost,
  fetchGetSavedFilmsOfUser,
  fetchSaveFilm,
  fetchSearchFilms,
  fetchGetUserInformations,
  fetchPostsByUserId,
};
