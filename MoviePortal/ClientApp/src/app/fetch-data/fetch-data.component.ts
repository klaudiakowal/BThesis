import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public movies: Movie[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Movie[]>(baseUrl + 'api/Movie/GetAllMovies').subscribe(result => {
      this.movies = result;
    }, error => console.error(error));
  }
}

interface Movie {
  releaseDate: string;
  rating: number;
  name: number;
  image: string;
}
