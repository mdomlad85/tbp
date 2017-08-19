#!/bin/bash

set -e
set -u

if [ $# != 2 ]; then
    echo "please enter a db host, and a user"
    exit 1
fi

export DBHOST=$1
export USER=$2


psql -U $USER -h $DBHOST -W -c "CREATE DATABASE tbpfoi WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'hr_HR.utf8' LC_CTYPE = 'hr_HR.utf8';"

for f in *.sql
do
	psql -U  $USER -h $DBHOST -W -d tbpfoi -a -f $f

	psql_exit_status=$?

	if [ $psql_exit_status != 0 ]; then
		echo "psql failed while trying to run this sql script" 1>&2
		exit $psql_exit_status
	fi
done

echo "sql script successful"
exit 0
