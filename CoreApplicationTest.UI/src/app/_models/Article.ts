import { Source } from './source';

export interface Article {
    author: string;
title: string;
description: string;
url: number;
urlToImage: string;
publishedAt: Date;
content: string;
source: Source;
}
