namespace XChecker.Core
{
    class PseudoCode
    {
        /*
         // not synchronized with the actual implementation, i.e. the implementation is right, but the pseudo code not.

        left file name is first in order
        a  b -> deleted
           c

        right file name is first in order
        d   a -> created
            b
            d

        if there is only a left file in queue
            fetch a right file
            compare the paths
            if the paths are the same
                do the magic of comparing
                if the files are the same
                    return "none"
                if the files differ
                    return "modified"
            if the left file name is the first in order
                mark the left file as "deleted"
                put the right file in queue
            if the right file name is the first in order
                mark the right file as "created"
                put the left file in queue

        if there is only a right file in queue
            fetch a left file
            compare the paths
            if the paths are the same
                do the magic of comparing
                if the files are the same
                    return "none"
                if the files differ
                    return "modified"
            if the left file name is the first in order
                mark the left file as "deleted"
                put the right file in queue
            if the right file name is the first in order
                mark the right file as "created"
                put the left file in queue

        if there are no files in queue
            fetch a left and one right file
            if there is a left file and there is a right file
                compare the paths
                if the paths are the same
                    do the magic of comparing
                    if the files are the same
                        return "none"
                    if the files differ
                        return "modified"
                if the left file name is the first in order
                    mark the left file as "deleted"
                    put the right file in queue
                if the right file name is the first in order
                    mark the right file as "created"
                    put the left file in queue
            if there is no left file and there is a right file
                mark all of the rest right files as "created"
            if there is left file and there is no right file
                mark all of the rest left files as "deleted"
            if there is no left file and there is no right file
                return false since there are no more files to compare
        */
    }
}
