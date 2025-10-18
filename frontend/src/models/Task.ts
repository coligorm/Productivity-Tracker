export interface Task {
    id: number;
    name: string;
    category: Category;
    score: Score;
}

export enum Category {
    "Mind",
    "Body",
    "Soul"
}

export enum Score {
    Low = 1,
    Medium = 2,
    High = 3
}