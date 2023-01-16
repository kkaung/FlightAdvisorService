import { Airport } from './airport';
import { Comment } from './comment';

export class City {
  id: number;

  comments?: Comment[];
  airports?: Airport[];

  constructor(
    public name: string,
    public description?: string,
    public country?: string
  ) {
    this.id = 0;
  }

  public static of(
    id: number,
    name: string,
    description: string,
    country: string,
    comments?: Comment[]
  ): City {
    const city = new City(name, description, country);
    city.id = id;

    if (comments) city.comments = comments;

    return city;
  }

  public toString = (): string => {
    return `City {Id:${this.id}, Name:${this.name}, Country: ${this.country},
        With ${this.comments ? this.comments.length : 0} comment(s)}`;
  };
}
