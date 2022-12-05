import { Tag } from "./tag.model"
export interface Post{
    id: number,
    title: string,
    summary: string,
    content: string,
    userId : number,
    postStatus: number,
    createdAt: Date,
    updatedAt: Date,
    authorName: string
}